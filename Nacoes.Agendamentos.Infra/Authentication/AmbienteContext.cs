using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Nacoes.Agendamentos.Application.Authentication.Context;
using Nacoes.Agendamentos.Application.Common.Enums;
using Nacoes.Agendamentos.Application.Common.Settings;
using Nacoes.Agendamentos.Infra.Extensions;

namespace Nacoes.Agendamentos.Infra.Authentication;

internal sealed class AmbienteContext(IHttpContextAccessor context,
                                      IOptions<AmbienteSettings> settings) 
    : IAmbienteContext
{
    private readonly AmbienteSettings _ambienteSettings = settings.Value;
    
    public string UserId => ClaimHelper.GetUserId(context);
    public bool IsUsuarioAuthenticated => ClaimHelper.GetIsAuthenticated(context);
    public bool IsUsuario => !IsBot && !IsThirdPartyUser;
    public bool IsBot => ClaimHelper.GetIsBot(context);
    public bool IsThirdPartyUser => ClaimHelper.GetIsThirdPartyUser(context);
    public EEnvironment GetEnvironment() => ClaimHelper.GetEnvironment(context);

    public void StartBotSession()
    {
        var claims = ClaimHelper.InvokeBot(_ambienteSettings.TipoEnum);
        var identity = new ClaimsIdentity(claims, "Bot");
        var principal = new ClaimsPrincipal(identity);
        context.SetUser(principal);
    }

    public void StartThirdPartyUserSession(string id, string? email)
    {
        var claims = ClaimHelper.InvokeThirdPartyUser(id, email, _ambienteSettings.TipoEnum);
        var identity = new ClaimsIdentity(claims, "ThirdPartyUser");
        var principal = new ClaimsPrincipal(identity);
        context.SetUser(principal);
    }
}