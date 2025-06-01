using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Security.Principal;

namespace Nacoes.Agendamentos.Infra.Extensions;

public static class HttpContextExtensions
{
    public static ClaimsPrincipal GetUsuario(this IHttpContextAccessor context)
    {
        return context.HttpContext?.User ?? throw new Exception("Erro ao obter usuário.");
    }

    public static IIdentity GetIdentity(this IHttpContextAccessor context)
    {
        return context.GetUsuario().Identity ?? throw new Exception("Erro ao obter identity");
    }

    public static Claim GetClaim(this IHttpContextAccessor context, string claimType)
    {
        return context.GetUsuario().FindFirst(claimType) ?? throw new Exception($"Erro ao obter claim {claimType}.");
    }
}