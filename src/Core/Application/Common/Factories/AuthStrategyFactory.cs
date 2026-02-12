using Microsoft.Extensions.DependencyInjection;
using Application.Authentication.Factories;
using Application.Authentication.Strategies;
using Domain.Usuarios;

namespace Application.Common.Factories;

internal class AuthStrategyFactory(IServiceProvider serviceProvider) : IAuthStrategyFactory
{
    public IAuthStrategy Criar(EAuthType tipo) => tipo switch
    {
        EAuthType.Local => serviceProvider.GetRequiredService<LocalAuthStrategy>(),
        EAuthType.Google => serviceProvider.GetRequiredService<GoogleAuthStrategy>(),
        _ => throw new NotImplementedException()
    };
}
