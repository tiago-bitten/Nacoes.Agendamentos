using Microsoft.Extensions.DependencyInjection;
using Nacoes.Agendamentos.Application.Authentication.Strategies;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;

namespace Nacoes.Agendamentos.Application.Authentication.Factories;

public class AuthStrategyFactory(IServiceProvider serviceProvider) : IAuthStrategyFactory
{
    public IAuthStrategy Criar(EAuthType tipo) => tipo switch
    {
        EAuthType.Local => serviceProvider.GetRequiredService<LocalAuthStrategy>(),
        EAuthType.Google => serviceProvider.GetRequiredService<GoogleAuthStrategy>(),
        _ => throw new NotImplementedException()
    };
}
