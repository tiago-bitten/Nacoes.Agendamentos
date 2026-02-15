using Domain.Usuarios;

namespace Application.Authentication.TokenGenerators;

public interface ITokenGenerator
{
    string GenerateAuth(User user);
    string GenerateRefresh();
}
