using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Nacoes.Agendamentos.Application.Authentication.TokenGenerators;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Infra.Helpers;
using Nacoes.Agendamentos.Infra.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Nacoes.Agendamentos.Application.Authentication.TokenGenerator;

public sealed class TokenGenerator(IOptions<AuthenticationSettings> authSettings) : ITokenGenerator
{
    private readonly JwtSettings _jwtSettings = authSettings.Value.Jwt;

    private DateTime DurationAuth => DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes);
    private byte[] Secret => Encoding.UTF8.GetBytes(_jwtSettings.Secret);

    #region GenerateAuth
    public string GenerateAuth(Usuario usuario)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var claims = ClaimHelper.Invoke(usuario);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DurationAuth,
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Secret), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    #endregion

    #region GenerateRefresh
    public string GenerateRefresh()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
    #endregion
}
