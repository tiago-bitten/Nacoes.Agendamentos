using Application.Authentication.Context;
using Application.Common.Enums;
using Domain.Shared.Factories;

namespace Application.Common.Factories;

internal sealed class LinkFactory(IAmbienteContext ambienteContext) : ILinkFactory
{
    public string Create(string path)
        => ambienteContext.Environment switch
        {
            EEnvironment.Local => $"http://localhost:5000/{path}",
            EEnvironment.Staging => $"https://staging.agendamentos.nacoes.com.br/{path}",
            EEnvironment.Production => $"https://agendamentos.nacoes.com.br/{path}",
            _ => throw new ArgumentOutOfRangeException()
        };
}
