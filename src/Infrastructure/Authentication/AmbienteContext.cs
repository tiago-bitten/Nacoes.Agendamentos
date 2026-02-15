using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Application.Authentication.Context;
using Application.Common.Enums;
using Application.Common.Settings;
using Domain.Enums;
using Infrastructure.Extensions;

namespace Infrastructure.Authentication;

internal sealed class EnvironmentContext(
    IHttpContextAccessor context,
    IOptions<EnvironmentSettings> settings)
    : IEnvironmentContext
{
    private readonly EnvironmentSettings _environmentSettings = settings.Value;

    public Guid UserId => ClaimHelper.GetUserId(context);
    public bool IsUserAuthenticated => ClaimHelper.GetIsAuthenticated(context);
    public EUserContextType UserContextType => ClaimHelper.GetUserContextType(context);
    public EEnvironment Environment => ClaimHelper.GetEnvironment(context);

    public void StartBotSession()
    {
        var claims = ClaimHelper.InvokeBot(_environmentSettings.GetTypeEnum());
        context.SetUser(claims, authenticationType: "Bot");
    }

    public void StartExternalSession(Guid id, string? email)
    {
        var claims = ClaimHelper.InvokeExternal(id, email, _environmentSettings.GetTypeEnum());
        context.SetUser(claims, authenticationType: "External");
    }
}
