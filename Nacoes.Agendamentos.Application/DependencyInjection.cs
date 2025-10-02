using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Nacoes.Agendamentos.Application.Abstracts.Behaviors;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Abstracts.Notifications;
using Nacoes.Agendamentos.Application.Authentication.Factories;
using Nacoes.Agendamentos.Application.Authentication.Strategies;
using Nacoes.Agendamentos.Application.Common.Factories;
using Nacoes.Agendamentos.Application.Generators.RecorrenciaEvento.Factories;
using Nacoes.Agendamentos.Application.Generators.RecorrenciaEvento.Strategies;
using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Common.Factories;

namespace Nacoes.Agendamentos.Application;

public static class DependencyInjection
{
    #region AddApplication
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddHandlers();
        services.AddValidators();
        services.AddFactories();
        services.AddServices();
        services.AddDecorators();
        
        return services;
    }
    #endregion
    
    #region AddHandlers
    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromAssembliesOf(typeof(DependencyInjection))
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

    #region AddValidators
    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);
        
        return services;
    }
    #endregion
    
    #region AddFactories
    private static IServiceCollection AddFactories(this IServiceCollection services)
    {
        services.AddScoped<IAuthStrategyFactory, AuthStrategyFactory>();
        services.AddScoped<ILinkFactory, LinkFactory>();
        services.AddScoped<IHorarioGeneratorFactory, HorarioGeneratorFactory>();

        return services;
    }
    #endregion

    #region AddServices
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<GoogleAuthStrategy>();
        services.AddScoped<LocalAuthStrategy>();

        services.AddScoped<HorarioDiarioGeneratorStrategy>();
        services.AddScoped<HorarioSemanalGeneratorStrategy>();
        services.AddScoped<HorarioMensalGeneratorStrategy>();
        services.AddScoped<HorarioAnualGeneratorStrategy>();
        
        return services;
    }
    #endregion
    
    #region AddDecorators
    private static IServiceCollection AddDecorators(this IServiceCollection services)
    {
        services.Decorate(typeof(ICommandHandler<,>), typeof(ValidationDecorator.CommandHandler<,>));
        services.Decorate(typeof(ICommandHandler<>), typeof(ValidationDecorator.CommandBaseHandler<>));
        
        services.Decorate(typeof(ICommandHandler<,>), typeof(TransactionDecorator.CommandHandler<,>));
        services.Decorate(typeof(ICommandHandler<>), typeof(TransactionDecorator.CommandBaseHandler<>));
        
        return services;
    }
    #endregion
}