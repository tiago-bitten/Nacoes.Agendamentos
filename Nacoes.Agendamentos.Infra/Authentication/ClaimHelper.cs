using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Nacoes.Agendamentos.Application.Common.Enums;
using Nacoes.Agendamentos.Infra.Extensions;

namespace Nacoes.Agendamentos.Infra.Authentication;

internal static class ClaimHelper
{
    #region Values
    public static readonly string IsAutenticado = "IsAutenticado";
    public static readonly string UserId = "UserId";
    public static readonly string UserEmailAddress = "UserEmailAddress";
    public static readonly string Environment = "Environment";
    public static readonly string IsBot = "IsBot";
    public static readonly string IsThirdPartyUser = "IsThirdPartyUser";    
    #endregion

    #region Invoke
    public static Claim[] InvokeUsuario(Guid id, string email, EEnvironment environment)
    {
        return GetContractClaims(id, email, isBot: false, isThirdPartyUser: false, environment);
    }

    public static Claim[] InvokeBot(EEnvironment environment)
    {
       return GetContractClaims(Guid.NewGuid(), "bot@nacoes.com", isBot: true, isThirdPartyUser: false, environment);
    }
    
    public static Claim[] InvokeThirdPartyUser(Guid id, string? email, EEnvironment environment)
    {
        return GetContractClaims(id, email ?? "voluntario-sem-email@nacoes.com", isBot: false, isThirdPartyUser: true,
            environment);
    }
    
    private static Claim[] GetContractClaims(
        Guid id, 
        string email, 
        bool isBot, 
        bool isThirdPartyUser, 
        EEnvironment environment)
    {
        return
        [
            new Claim(UserId, id.ToString()),
            new Claim(UserEmailAddress, email),
            new Claim(IsBot, isBot.ToString()),
            new Claim(IsThirdPartyUser, isThirdPartyUser.ToString()),
            new Claim(Environment, ((int)environment).ToString())
        ];
    }
    #endregion

    #region GetIsAuthenticated
    public static bool GetIsAuthenticated(IHttpContextAccessor context)
    {
        return context.GetIdentity().IsAuthenticated;
    }
    #endregion

    #region GetUserId
    public static Guid GetUserId(IHttpContextAccessor context)
    {
        return Guid.Parse(context.GetClaim(UserId).Value);
    }
    #endregion

    #region GetUserEmailAddress
    public static string GetUserEmailAddress(IHttpContextAccessor context)
    {
        return context.GetClaim(UserEmailAddress).Value;
    }
    #endregion
    
    #region GetIsBot
    public static bool GetIsBot(IHttpContextAccessor context)
    {
        return bool.Parse(context.GetClaim(IsBot).Value);
    }
    #endregion
    
    #region GetIsThirdPartyUser
    public static bool GetIsThirdPartyUser(IHttpContextAccessor context)
    {
        return bool.Parse(context.GetClaim(IsThirdPartyUser).Value);
    }
    #endregion

    #region GetEnvironment
    public static EEnvironment GetEnvironment(IHttpContextAccessor context)
    {
        return (EEnvironment)int.Parse(context.GetClaim(Environment).Value);
    }
    #endregion
}