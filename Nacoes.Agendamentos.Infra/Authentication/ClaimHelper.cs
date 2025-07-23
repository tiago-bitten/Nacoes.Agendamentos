using System.Security.Claims;
using Microsoft.AspNetCore.Http;
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
    public static Claim[] InvokeUsuario(string id, string email)
    {
        return
        [
            new Claim(UserId, id),
            new Claim(UserEmailAddress, email),
            new Claim(IsBot, bool.FalseString),
            new Claim(IsThirdPartyUser, bool.FalseString)
        ];
    }

    public static Claim[] InvokeBot()
    {
        return
        [
            new Claim(UserId, "1"),
            new Claim(UserEmailAddress, "bot@nacoes.com"),
            new Claim(IsBot, bool.TrueString),
            new Claim(IsThirdPartyUser, bool.FalseString)
        ];
    }
    
    public static Claim[] InvokeThirdPartyUser(string id, string? email)
    {
        return
        [
            new Claim(UserId, id),
            new Claim(UserEmailAddress, email ?? "voluntario-sem-email@nacoes.com"),
            new Claim(IsBot, bool.FalseString),
            new Claim(IsThirdPartyUser, bool.TrueString)
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
    public static string GetUserId(IHttpContextAccessor context)
    {
        return context.GetClaim(UserId).Value;
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

    //#region GetEnvironment
    //public static EEnvironment GetEnvironment(IHttpContextAccessor context)
    //{
    //    return (EEnvironment)int.Parse(context.GetClaim(Environment).Value);
    //}
    //#endregion
}