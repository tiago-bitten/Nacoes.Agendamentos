using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Nacoes.Agendamentos.Application.Authentication.Context;
using Nacoes.Agendamentos.Application.Common.Enums;
using Nacoes.Agendamentos.Application.Common.Settings;
using Nacoes.Agendamentos.Domain.Enums;
using Nacoes.Agendamentos.Infra.Extensions;

namespace Nacoes.Agendamentos.Infra.Authentication;

internal sealed class AmbienteContext(
    IHttpContextAccessor context, 
    IOptions<AmbienteSettings> settings) 
    : IAmbienteContext
{
    private readonly AmbienteSettings _ambienteSettings = settings.Value;
    
    public Guid UserId => ClaimHelper.GetUserId(context);
    public bool IsUserAuthenticated => ClaimHelper.GetIsAuthenticated(context);
    public EUserContextType UserContextType => ClaimHelper.GetUserContextType(context);
    public EEnvironment Environment => ClaimHelper.GetEnvironment(context);

    public void StartBotSession()
    {
        var claims = ClaimHelper.InvokeBot(_ambienteSettings.TipoEnum);
        context.SetUser(claims, authenticationType: "Bot");
    }

    public void StartExternalSession(Guid id, string? email)
    {
        var claims = ClaimHelper.InvokeExternal(id, email, _ambienteSettings.TipoEnum);
        context.SetUser(claims, authenticationType: "External");
    }
}