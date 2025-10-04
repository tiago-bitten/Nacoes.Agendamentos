using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Nacoes.Agendamentos.Application.Common.Enums;
using Nacoes.Agendamentos.Domain.Enums;
using Nacoes.Agendamentos.Infra.Extensions;

namespace Nacoes.Agendamentos.Infra.Authentication;

internal static class ClaimHelper
{
    public static readonly string UserId = "UserId";
    public static readonly string UserEmailAddress = "UserEmailAddress";
    public static readonly string UserContextType = "UserContextType";
    public static readonly string Environment = "Environment";
    
    public static readonly Guid BotId = Guid.Empty;
    public static readonly string BotEmail = "bot@nacoes.com";
    public static readonly string ExternalEmail = "voluntario-sem-email@nacoes.com";

    public static Claim[] InvokeUsuario(Guid id, string email, EEnvironment environment)
    {
        return BuildClaims(id, email, EUserContextType.Usuario, environment);
    }

    public static Claim[] InvokeBot(EEnvironment environment)
    {
       return BuildClaims(BotId, BotEmail, EUserContextType.Bot, environment);
    }
    
    public static Claim[] InvokeExternal(Guid id, string? email, EEnvironment environment)
    {
        return BuildClaims(id, email ?? ExternalEmail, EUserContextType.External, environment);
    }
    
    private static Claim[] BuildClaims(
        Guid id, 
        string email, 
        EUserContextType userContextType,
        EEnvironment environment)
    {
        return
        [
            new Claim(UserId, id.ToString()),
            new Claim(UserEmailAddress, email),
            new Claim(UserContextType, ((int)userContextType).ToString()),
            new Claim(Environment, ((int)environment).ToString())
        ];
    }

    public static bool GetIsAuthenticated(IHttpContextAccessor context)
    {
        return context.GetIdentity().IsAuthenticated;
    }

    public static Guid GetUserId(IHttpContextAccessor context)
    {
        return Guid.Parse(context.GetClaim(UserId).Value);
    }

    public static string GetUserEmailAddress(IHttpContextAccessor context)
    {
        return context.GetClaim(UserEmailAddress).Value;
    }
    
    public static EUserContextType GetUserContextType(IHttpContextAccessor context)
    {
        return (EUserContextType)int.Parse(context.GetClaim(UserContextType).Value);
    }

    public static EEnvironment GetEnvironment(IHttpContextAccessor context)
    {
        return (EEnvironment)int.Parse(context.GetClaim(Environment).Value);
    }
}