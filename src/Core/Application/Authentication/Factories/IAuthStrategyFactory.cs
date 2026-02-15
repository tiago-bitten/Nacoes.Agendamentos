using Application.Authentication.Strategies;
using Domain.Usuarios;

namespace Application.Authentication.Factories;

public interface IAuthStrategyFactory
{
    IAuthStrategy Create(EAuthType type);
}
