using Nacoes.Agendamentos.Domain.Entities.Usuarios;

namespace Nacoes.Agendamentos.Application.Authentication.TokenGenerators;

public interface ITokenGenerator
{
    string GenerateAuth(Usuario usuario);
    string GenerateRefresh();
}
