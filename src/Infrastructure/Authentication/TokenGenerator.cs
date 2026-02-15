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
    IOptions<EnvironmentSettings> environmentSettings) : ITokenGenerator
{
    private readonly JwtSettings _jwtSettings = authSettings.Value.Jwt;
    private readonly EnvironmentSettings _environmentSettings = environmentSettings.Value;

    private DateTimeOffset DurationAuth => DateTimeOffset.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes);
    private byte[] Secret => Encoding.UTF8.GetBytes(_jwtSettings.Secret);

    public string GenerateAuth(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var claims = ClaimHelper.InvokeUser(user.Id, user.Email.Address, _environmentSettings.GetTypeEnum());

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DurationAuth.DateTime,
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Secret),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string GenerateRefresh()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
