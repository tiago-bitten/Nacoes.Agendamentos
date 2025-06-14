﻿using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Nacoes.agendamentos.application.entities.agendas.commands.agendar;
using Nacoes.Agendamentos.Application.Authentication.Commands.Login;
using Nacoes.Agendamentos.Application.Authentication.Factories;
using Nacoes.Agendamentos.Application.Authentication.Strategies;
using Nacoes.Agendamentos.Application.Authentication.TokenGenerator;
using Nacoes.Agendamentos.Application.Authentication.TokenGenerators;
using Nacoes.Agendamentos.Application.Entities.Agendas.Commands.AdicionarAgenda;
using Nacoes.Agendamentos.Application.Entities.Agendas.Commands.Agendar;
using Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.AdicionarAtividade;
using Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.AdicionarMinisterio;
using Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.AdicionarUsuario;
using Nacoes.Agendamentos.Application.Entities.Usuarios.UseCases.AdicionarUsuarioUseCase;
using Nacoes.Agendamentos.Application.Entities.Usuarios.Validators;
using Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.AdicionarVoluntario;
using Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.VincularVoluntarioMinisterio;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Agendas.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Ministerios.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Interfaces;
using Nacoes.Agendamentos.Infra.Contexts;
using Nacoes.Agendamentos.Infra.Entities.Agendas;
using Nacoes.Agendamentos.Infra.Entities.Ministerios;
using Nacoes.Agendamentos.Infra.Entities.Usuarios;
using Nacoes.Agendamentos.Infra.Entities.Voluntarios;
using Nacoes.Agendamentos.Infra.Persistence;
using Nacoes.Agendamentos.Infra.Settings;
using Nacoes.Agendamentos.ReadModels.Entities.Usuarios.Queries.RecuperarUsuarios;
using Nacoes.Agendamentos.ReadModels.Entities.Voluntarios.Queries.RecuperarVoluntario;

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
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
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

        return services;
    }
    #endregion

    #region AddAppHandlers
    public static IServiceCollection AddAppHandlers(this IServiceCollection services)
    {
        services.AddScoped<IAdicionarUsuarioHandler, AdicionarUsuarioHandler>();
        services.AddScoped<IAdicionarMinisterioHandler, AdicionarMinisterioHandler>();
        services.AddScoped<ILoginHandler, LoginHandler>();
        services.AddScoped<IAdicionarVoluntarioHandler, AdicionarVoluntarioHandler>();
        services.AddScoped<IAdicionarAtivdadeHandler, AdicionarAtividadeHandler>();
        services.AddScoped<IAdicionarAgendaHandler, AdicionarAgendaHandler>();
        services.AddScoped<IVincularVoluntarioMinisterioHandler, VincularVoluntarioMinisterioHandler>();
        services.AddScoped<IAgendarHandler, AgendarHandler>();

        return services;
    }
    #endregion

    #region AddAppQueries
    public static IServiceCollection AddAppQueries(this IServiceCollection services)
    {
        services.AddScoped<IRecuperarUsuarioQuery, RecuperarUsuarioQuery>();
        services.AddScoped<IRecuperarVoluntarioQuery, RecuperarVoluntarioQuery>();

        return services;
    }
    #endregion

    #region AddValidators
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<AdicionarUsuarioCommandValidator>();

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
}