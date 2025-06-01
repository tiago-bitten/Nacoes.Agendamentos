using Nacoes.Agendamentos.Application.Authentication.Strategies;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;

namespace Nacoes.Agendamentos.Application.Authentication.Factories;

public interface IAuthStrategyFactory
{
    IAuthStrategy Criar(EAuthType tipo);
}