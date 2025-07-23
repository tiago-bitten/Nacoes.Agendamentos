using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Nacoes.Agendamentos.Application.Authentication.Context;
using Nacoes.Agendamentos.Application.Common.Responses;
using Nacoes.Agendamentos.Application.Entities.Voluntarios.Interfaces;
using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Application.Common.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class VoluntarioAuthorizationAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var httpContext = context.HttpContext;

        if (!httpContext.Request.Headers.TryGetValue("cpf", out var cpf) ||
            !httpContext.Request.Headers.TryGetValue("data-nascimento", out var dataNascimentoStr))
        {
            httpContext.Response.StatusCode = 401;
            httpContext.Response.ContentType = "application/json";
            var error = new Error("Voluntario.Login", ErrorType.Unauthorized, "Cpf e/ou data de nascimento não informados.");
            await httpContext.Response.WriteAsJsonAsync(ApiResponse.Erro(error));
            return;
        }

        var dataNascimento = DateOnly.Parse(dataNascimentoStr!);

        var ambienteContext = httpContext.RequestServices.GetRequiredService<IAmbienteContext>();
        var voluntarioAppRepository = httpContext.RequestServices.GetRequiredService<IVoluntarioApplicationRepository>();

        var loginDto = await voluntarioAppRepository.RecuperarLoginAsync(cpf!, dataNascimento);
        if (loginDto is null)
        {
            httpContext.Response.StatusCode = 401;
            httpContext.Response.ContentType = "application/json";
            var error = new Error("Voluntario.Login", ErrorType.Unauthorized, "Não foi possível localizar seu cadastro.");
            await httpContext.Response.WriteAsJsonAsync(ApiResponse.Erro(error));
            return;
        }

        ambienteContext.StartThirdPartyUserSession(loginDto.VoluntarioId, loginDto.VoluntarioEmail);

        await next();
    }
}

