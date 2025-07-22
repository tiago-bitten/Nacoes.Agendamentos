using System.Text;
using FluentValidation;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Nacoes.Agendamentos.Application.Abstracts.BackgroundJobs;
using Nacoes.Agendamentos.Application.Abstracts.CronJobs;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.agendamentos.application.entities.agendas.commands.agendar;
using Nacoes.Agendamentos.Application.Authentication.Commands.Login;
using Nacoes.Agendamentos.Application.Authentication.Context;
using Nacoes.Agendamentos.Application.Authentication.Factories;
using Nacoes.Agendamentos.Application.Authentication.Strategies;
using Nacoes.Agendamentos.Application.Authentication.TokenGenerators;
using Nacoes.Agendamentos.Application.Authentication.Validators;
using Nacoes.Agendamentos.Application.Common.Settings;
using Nacoes.Agendamentos.Application.Entities.Agendas.Commands.AdicionarAgenda;
using Nacoes.Agendamentos.Application.Entities.Agendas.Commands.Agendar;
using Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.AdicionarAtividade;
using Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.AdicionarMinisterio;
using Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.Adicionar;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Agendas.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Historicos.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Ministerios.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Interfaces;
using Nacoes.Agendamentos.Infra.Authentication;
using Nacoes.Agendamentos.Infra.BackgroundJobs;
using Nacoes.Agendamentos.Infra.Contexts;
using Nacoes.Agendamentos.Infra.CronJobs.Implementations;
using Nacoes.Agendamentos.Infra.Entities.Agendas;
using Nacoes.Agendamentos.Infra.Entities.Historicos;
using Nacoes.Agendamentos.Infra.Entities.Ministerios;
using Nacoes.Agendamentos.Infra.Entities.Usuarios;
using Nacoes.Agendamentos.Infra.Entities.Voluntarios;
using Nacoes.Agendamentos.Infra.Persistence;

namespace Nacoes.Agendamentos.API.Extensions;

public static class DependencyInjectionExtensions
{
    #region AddAppConfiguration
    public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<DatabaseSettings>()
                .Bind(configuration.GetSection("ConnectionStrings"))
                .ValidateDataAnnotations()
                .ValidateOnStart();

        services.AddOptions<AuthenticationSettings>()
                .Bind(configuration.GetSection("Authentication"))
                .ValidateDataAnnotations()
                .ValidateOnStart();

        services.AddSingleton(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<AuthenticationSettings>>().Value);

        return services;
    }
    #endregion

    #region AddDatabase
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<NacoesDbContext>((sp, options) =>
        {
            var dbSettings = sp.GetRequiredService<DatabaseSettings>();

            options.UseNpgsql(dbSettings.Postgres)
                   .UseSnakeCaseNamingConvention();
        });

        return services;
    }
    #endregion

    #region AddRepositories
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
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
    #endregion

    #region AddValidators
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<LoginCommandValidator>();

        return services;
    }
    #endregion

    #region AddFactories
    public static IServiceCollection AddFactories(this IServiceCollection services)
    {
        services.AddScoped<IAuthStrategyFactory, AuthStrategyFactory>();

        return services;
    }
    #endregion

    #region AddServices
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenGenerator, TokenGenerator>();
        services.AddScoped<GoogleAuthStrategy>();
        services.AddScoped<LocalAuthStrategy>();
        services.AddScoped<IHistoricoRegister, HistoricoRegister>();

        services.AddScoped<IAmbienteContext, AmbienteContext>();
        
        var infraAssembly = typeof(DailyAppInfoJob).Assembly;
        services.Scan(scan => scan.FromAssemblies(infraAssembly)
                .AddClasses(classes => classes.AssignableTo<ICronJob>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());

        return services;
    }
    #endregion

    #region Jwt
    public static IServiceCollection AddJwt(this IServiceCollection services, string issuer, string audience, string secretKey)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                    };
                });

        return services;
    }
    #endregion
    
    #region AddAppHandlers
    public static IServiceCollection AddAppHandlers(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromAssembliesOf(typeof(AdicionarUsuarioCommand))
                .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime());

        return services;
    }
    #endregion
    
    #region AddHangfire
    public static IServiceCollection AddHangfire(this IServiceCollection services)
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

        return services;
    }
    #endregion
}