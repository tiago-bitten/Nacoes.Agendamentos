using Microsoft.AspNetCore.Authorization;

namespace Nacoes.Agendamentos.API.Controllers.Abstracts;

//[Authorize]
public abstract class NacoesAuthenticatedController : NacoesController
{
}
