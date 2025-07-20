using Microsoft.AspNetCore.Http;
using Nacoes.Agendamentos.Application.Authentication.Context;
using Nacoes.Agendamentos.Infra.Helpers;

namespace Nacoes.Agendamentos.Infra.Authentication;

public sealed class AmbienteContext(IHttpContextAccessor context) : IAmbienteContext
{
    public Guid UsuarioId => ClaimHelper.GetUsusarioId(context);
}