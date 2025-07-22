using System.Security.Claims;
using Google.Apis.Util;
using Microsoft.AspNetCore.Http;
using Nacoes.Agendamentos.Application.Authentication.Context;
using Nacoes.Agendamentos.Infra.Extensions;
using Nacoes.Agendamentos.Infra.Helpers;

namespace Nacoes.Agendamentos.Infra.Authentication;

public sealed class AmbienteContext(IHttpContextAccessor context) : IAmbienteContext
{
    public Guid UsuarioId => ClaimHelper.GetUsusarioId(context);
    public bool IsUsuarioAuthenticated => ClaimHelper.GetIsAuthenticated(context);
    public bool IsBot => ClaimHelper.GetIsBot(context);

    public void StartBotSession()
    {
        var claims = ClaimHelper.InvokeBot();
        var identity = new ClaimsIdentity(claims, "Bot");
        var principal = new ClaimsPrincipal(identity);
        context.SetUser(principal);
    }
}