using System.Text;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Application.Shared.Ports.BackgroundJobs;
using Application.Shared.Ports.CronJobs;
using Application.Shared.Ports.Notifications;
using Application.Authentication.Context;
using Application.Authentication.TokenGenerators;
using Application.Common.DateTime;
using Application.Common.Settings;
using Application.Generators.RecorrenciaEvento;
using Domain.Shared.Events;
using Domain.Historicos.Interfaces;
using Infrastructure.Authentication;
using Infrastructure.BackgroundJobs;
using Infrastructure.Common.DateTime;
using Infrastructure.CronJobs;
using Infrastructure.Generators;
using Infrastructure.Notifications;
using Infrastructure.Notifications.Emails;
using Infrastructure.Repositories;
using Postgres;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddProjectDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSettings(configuration);
        services.AddApplication();

        var dbSettings = configuration.GetSection("ConnectionStrings").Get<DatabaseSettings>()!;
        services.AddPostgresAdapter(dbSettings.Postgres);

        services.AddInfrastructure(configuration);

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

        return services;
    }

    private static IServiceCollection AddApplication(this IServiceCollection services)
    {
        Application.DependencyInjection.AddApplication(services);

        services.Scan(scan => scan.FromAssembliesOf(typeof(Application.DependencyInjection))
                .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime());

        return services;
    }

    private static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddServices();
        services.AddAuthenticationInternal(configuration);
        services.AddRepositories();
        services.AddHangfireInternal();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IHistoricoRegister, HistoricoRegister>();

        services.AddScoped<ITemplateRenderer, TemplateRenderer>();

        services.AddScoped<ITimeZoneManager, TimeZoneManager>();

        services.AddScoped<IRecorrenciaEventoManager, RecorrenciaEventoManager>();

        var infraAssembly = typeof(DependencyInjection).Assembly;
        services.Scan(scan => scan.FromAssemblies(infraAssembly)
                .AddClasses(classes => classes.AssignableTo<ICronJob>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());

        services.AddScoped<IEmailSenderFactory, EmailSenderFactory>();

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
        services.AddScoped<IQueryExecutor, QueryExecutor>();
        services.AddScoped<ICronJobScheduler, HangfireCronJobScheduler>();

        services.AddHangfireServer();

        return services;
    }
}
