using Microsoft.AspNetCore.Http;
using Nacoes.Agendamentos.Infra.Helpers;

namespace Nacoes.Agendamentos.Application.Authentication.Context;

public sealed class AmbienteContext(IHttpContextAccessor context) : IAmbienteContext
{
    public Guid UsuarioId => ClaimHelper.GetUsusarioId(context);
}