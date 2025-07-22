using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Nacoes.Agendamentos.Application.Authentication.Context;
using Nacoes.Agendamentos.Application.Entities.Voluntarios.Interfaces;

namespace Nacoes.Agendamentos.Application.Common.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class VoluntarioAutenticadoAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var httpContext = context.HttpContext;

        if (!httpContext.Request.Headers.TryGetValue("cpf", out var cpf) ||
            !httpContext.Request.Headers.TryGetValue("data-nascimento", out var dataNascimentoStr))
        {
            httpContext.Response.StatusCode = 401;
            return;
        }

        var dataNascimento = DateOnly.Parse(dataNascimentoStr!);

        var ambienteContext = httpContext.RequestServices.GetRequiredService<IAmbienteContext>();
        var voluntarioAppRepository = httpContext.RequestServices.GetRequiredService<IVoluntarioApplicationRepository>();

        var loginDto = await voluntarioAppRepository.RecuperarLoginAsync(cpf!, dataNascimento);
        if (loginDto is null)
        {
            httpContext.Response.StatusCode = 401;
            return;
        }

        ambienteContext.StartThirdPartyUserSession(loginDto.VoluntarioId, loginDto.VoluntarioEmail);

        await next();
    }
}

