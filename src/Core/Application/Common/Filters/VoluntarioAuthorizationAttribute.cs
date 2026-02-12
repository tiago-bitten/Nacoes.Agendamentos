using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Application.Shared.Messaging;
using Application.Authentication.Commands.LoginExterno;
using Application.Authentication.Context;
using Application.Common.Responses;
using Domain.Voluntarios.Errors;

namespace Application.Common.Filters;

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
            var error = VoluntarioErrors.AutenticacaoInvalida;
            await httpContext.Response.WriteAsJsonAsync(ApiResponse.Erro(error));
            return;
        }

        var dataNascimento = DateOnly.Parse(dataNascimentoStr!);

        var loginHandler = httpContext.RequestServices.GetRequiredService<ICommandHandler<LoginExternoCommand>>();

        var loginResult = await loginHandler.HandleAsync(new LoginExternoCommand(cpf!, dataNascimento));

        if (loginResult.IsFailure)
        {
            httpContext.Response.StatusCode = 401;
            httpContext.Response.ContentType = "application/json";
            var error = VoluntarioErrors.VoluntarioLoginNaoEncontrado;
            await httpContext.Response.WriteAsJsonAsync(ApiResponse.Erro(error));
            return;
        }

        await next();
    }
}
