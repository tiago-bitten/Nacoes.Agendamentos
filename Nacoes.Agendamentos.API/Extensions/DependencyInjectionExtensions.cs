using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Nacoes.Agendamentos.Application.Entities.Usuarios.UseCases.AdicionarUsuarioUseCase;
using Nacoes.Agendamentos.Application.Entities.Usuarios.Validators;
using Nacoes.Agendamentos.Domain.Entities.Agendas.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Ministerios.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.VoluntariosMinisterios.Interfaces;
using Nacoes.Agendamentos.Infra.Contexts;
using Nacoes.Agendamentos.Infra.Entities.Agendas;
using Nacoes.Agendamentos.Infra.Entities.Ministerios;
using Nacoes.Agendamentos.Infra.Entities.Usuarios;
using Nacoes.Agendamentos.Infra.Entities.Voluntarios;
using Nacoes.Agendamentos.Infra.Entities.VoluntariosMinisterios;
using Nacoes.Agendamentos.Infra.Settings;

namespace Nacoes.Agendamentos.API.IoC;

public static class DependencyInjectionExtensions
{
    #region AddAppConfiguration
    public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<DatabaseSettings>()
                .Bind(configuration.GetSection("ConnectionStrings"))
                .ValidateDataAnnotations()
                .ValidateOnStart();

        services.AddSingleton(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

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
        services.AddScoped<IAgendaRepository, AgendaRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IMinisterioRepository, MinisterioRepository>();
        services.AddScoped<IVoluntarioRepository, VoluntarioRepository>();
        services.AddScoped<IVoluntarioMinisterioRepository, VoluntarioMinisterioRepository>();

        return services;
    }
    #endregion

    #region AddAppHandlers
    public static IServiceCollection AddAppHandlers(this IServiceCollection services)
    {
        services.AddScoped<IAdicionarUsuarioHandler, AdicionarUsuarioHandler>();

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
}