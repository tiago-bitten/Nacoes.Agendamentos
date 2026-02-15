using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Application.Authentication.TokenGenerators;
using Application.Common.Settings;
using Domain.Usuarios;

namespace Infrastructure.Authentication;

internal sealed class TokenGenerator(
    IOptions<AuthenticationSettings> authSettings,
    IOptions<AmbienteSettings> ambienteSettings) : ITokenGenerator
{
    private readonly JwtSettings _jwtSettings = authSettings.Value.Jwt;
    private readonly AmbienteSettings _ambienteSettings = ambienteSettings.Value;

    private DateTimeOffset DurationAuth => DateTimeOffset.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes);
    private byte[] Secret => Encoding.UTF8.GetBytes(_jwtSettings.Secret);

    #region GenerateAuth
    public string GenerateAuth(Usuario usuario)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var claims = ClaimHelper.InvokeUsuario(usuario.Id, usuario.Email.Address, _ambienteSettings.GetTipoEnum());

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DurationAuth.DateTime,
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
