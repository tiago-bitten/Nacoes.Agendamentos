using Microsoft.Extensions.DependencyInjection;
using Application.Authentication.Factories;
using Application.Authentication.Strategies;
using Domain.Usuarios;

namespace Application.Common.Factories;

internal sealed class AuthStrategyFactory(IServiceProvider serviceProvider) : IAuthStrategyFactory
{
    public IAuthStrategy Create(EAuthType type) => type switch
    {
        EAuthType.Local => serviceProvider.GetRequiredService<LocalAuthStrategy>(),
        EAuthType.Google => serviceProvider.GetRequiredService<GoogleAuthStrategy>(),
        _ => throw new NotImplementedException()
    };
}
