using System.Text;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Nacoes.Agendamentos.Application.Abstracts.BackgroundJobs;
using Nacoes.Agendamentos.Application.Abstracts.CronJobs;
using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Application.Abstracts.Notifications;
using Nacoes.Agendamentos.Application.Authentication.Context;
using Nacoes.Agendamentos.Application.Authentication.TokenGenerators;
using Nacoes.Agendamentos.Application.Common.DateTime;
using Nacoes.Agendamentos.Application.Common.Settings;
using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Agendas.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Historicos.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Ministerios.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Interfaces;
using Nacoes.Agendamentos.Infra.Authentication;
using Nacoes.Agendamentos.Infra.BackgroundJobs;
using Nacoes.Agendamentos.Infra.Common.DateTime;
using Nacoes.Agendamentos.Infra.Contexts;
using Nacoes.Agendamentos.Infra.Entities.Agendas;
using Nacoes.Agendamentos.Infra.Entities.DomainEvents;
using Nacoes.Agendamentos.Infra.Entities.Historicos;
using Nacoes.Agendamentos.Infra.Entities.Ministerios;
using Nacoes.Agendamentos.Infra.Entities.Usuarios;
using Nacoes.Agendamentos.Infra.Entities.Voluntarios;
using Nacoes.Agendamentos.Infra.Notifications;
using Nacoes.Agendamentos.Infra.Notifications.Emails;

namespace Nacoes.Agendamentos.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSettings(configuration);
        services.AddServices();
        services.AddDatabase();
        services.AddAuthenticationInternal(configuration);
        services.AddRepositories();
        services.AddHangfireInternal();

        return services;
    }

    private static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<DatabaseSettings>()
                .Bind(configuration.GetSection("ConnectionStrings"))
                .ValidateDataAnnotations()
                .ValidateOnStart();

        services.AddOptions<AuthenticationSettings>()
                .Bind(configuration.GetSection("Authentication"))
                .ValidateDataAnnotations()
                .ValidateOnStart();
        
        services.AddOptions<DevSettings>()
                .Bind(configuration.GetSection("Dev"))
                .ValidateDataAnnotations()
                .ValidateOnStart();
        
        services.AddOptions<AmbienteSettings>()
                .Bind(configuration.GetSection("Ambiente"))
                .ValidateDataAnnotations()
                .ValidateOnStart();
        
        services.AddOptions<NotificationsSettings>()
                .Bind(configuration.GetSection("Notifications"))
                .ValidateDataAnnotations()
                .ValidateOnStart();

        services.AddSingleton(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<AuthenticationSettings>>().Value);
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<DevSettings>>().Value);

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IDomainEventsDispatcher, DomainEventsDispatcher>();
        
        services.Scan(scan => scan.FromAssembliesOf(typeof(Application.DependencyInjection))
                .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime());

        services.AddScoped<IHistoricoRegister, HistoricoRegister>();
        
        services.AddScoped<ITemplateRenderer, TemplateRenderer>();

        services.AddScoped<ITimeZoneManager, TimeZoneManager>();
        
        var infraAssembly = typeof(DependencyInjection).Assembly;
        services.Scan(scan => scan.FromAssemblies(infraAssembly)
                .AddClasses(classes => classes.AssignableTo<ICronJob>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());

        services.AddScoped<IEmailSenderFactory, EmailSenderFactory>();
        
        return services;
    }
    
    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<NacoesDbContext>((sp, options) =>
        {
            var dbSettings = sp.GetRequiredService<DatabaseSettings>();
            
            options.UseNpgsql(dbSettings.Postgres)
                   .UseSnakeCaseNamingConvention();
        });
        
        services.AddScoped<INacoesDbContext>(sp => sp.GetRequiredService<NacoesDbContext>());
        
        var devSettings = services.BuildServiceProvider().GetRequiredService<DevSettings>();
        if (devSettings.RecriarBanco)
        {
            using var scope = services.BuildServiceProvider().CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<NacoesDbContext>();
            var entityTypes = context.Model.GetEntityTypes();

            foreach (var type in entityTypes)
            {
                var tableName = type.GetTableName();
                var sql = $"TRUNCATE TABLE {tableName} CASCADE;";
                context.Database.ExecuteSqlRaw(sql);
                
                Console.WriteLine($"Tabela {tableName} truncada.");
            }
            context.SaveChanges();
        }
        
        return services;
    }

    private static IServiceCollection AddAuthenticationInternal(this IServiceCollection services, IConfiguration configuration)
    {
        var authSettings = configuration.GetSection("Authentication").Get<AuthenticationSettings>()!;
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = authSettings.Jwt.Issuer,
                    ValidAudience = authSettings.Jwt.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.Jwt.Secret)),
                    ClockSkew = TimeSpan.Zero
                };
                
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = ctx =>
                    {
                        Console.WriteLine("=== JWT Authentication Failed ===");
                        Console.WriteLine(ctx.Exception.ToString());
                        return Task.CompletedTask;
                    },
                    OnChallenge = ctx =>
                    {
                        Console.WriteLine("=== JWT Challenge ===");
                        Console.WriteLine(ctx.Error);
                        Console.WriteLine(ctx.ErrorDescription);
                        return Task.CompletedTask;
                    },
                    OnForbidden = ctx =>
                    {
                        Console.WriteLine("=== JWT Forbidden ===");
                        return Task.CompletedTask;
                    }
                };
            });

        services.AddHttpContextAccessor();
        services.AddScoped<IAmbienteContext, AmbienteContext>();
        services.AddSingleton<ITokenGenerator, TokenGenerator>();
        
        return services;
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAgendaRepository, AgendaRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IMinisterioRepository, MinisterioRepository>();
        services.AddScoped<IVoluntarioRepository, VoluntarioRepository>();
        services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();
        services.AddScoped<IVoluntarioMinisterioRepository, VoluntarioMinisterioRepository>();
        services.AddScoped<IAtividadeRepository, AtividadeRepository>();
        services.AddScoped<IUsuarioConviteRepository, UsuarioConviteRepository>();
        services.AddScoped<IHistoricoRepository, HistoricoRepository>();
        
        return services;
    }
    
    private static IServiceCollection AddHangfireInternal(this IServiceCollection services)
    {
        services.AddHangfire((sp, config) =>
        {
            var dbSettings = sp.GetRequiredService<DatabaseSettings>();

            config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                  .UseSimpleAssemblyNameTypeSerializer()
                  .UseRecommendedSerializerSettings()
                  .UsePostgreSqlStorage(options =>
                      {
                          options.UseNpgsqlConnection(dbSettings.Postgres);
                      },
                      new PostgreSqlStorageOptions
                      {
                          SchemaName = "Hangfire"
                      });
        });

        services.AddScoped<IBackgroundJobManager, HangfireBackgroundJobManager>();
        services.AddTransient<BackgroundJobProcessor>();
        services.AddScoped<ICommandExecutor, CommandExecutor>();
        
        services.AddHangfireServer();

        return services;
    }
}