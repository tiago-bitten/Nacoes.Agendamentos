using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Security.Principal;

namespace Nacoes.Agendamentos.Infra.Extensions;

internal static class HttpContextExtensions
{
    public static ClaimsPrincipal GetUser(this IHttpContextAccessor context)
    {
        return context.HttpContext?.User ?? throw new Exception("Erro ao obter usuário.");
    }
    
    public static void SetUser(this IHttpContextAccessor context, ClaimsPrincipal user)
    {
        context.GetUser();
        context.HttpContext!.User = user;
    }

    public static IIdentity GetIdentity(this IHttpContextAccessor context)
    {
        return context.GetUser().Identity ?? throw new Exception("Erro ao obter identity");
    }

    public static Claim GetClaim(this IHttpContextAccessor context, string claimType)
    {
        return context.GetUser().FindFirst(claimType) ?? throw new Exception($"Erro ao obter claim {claimType}.");
    }
}