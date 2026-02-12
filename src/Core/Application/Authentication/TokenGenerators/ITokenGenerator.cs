using Domain.Usuarios;

namespace Application.Authentication.TokenGenerators;

public interface ITokenGenerator
{
    string GenerateAuth(Usuario usuario);
    string GenerateRefresh();
}
