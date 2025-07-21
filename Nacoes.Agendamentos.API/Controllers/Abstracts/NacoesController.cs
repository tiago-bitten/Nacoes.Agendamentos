using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Nacoes.Agendamentos.Application.Common.Responses;
using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.API.Controllers.Abstracts;

[Route("api/[controller]")]
[ApiController]
public abstract class NacoesController : ControllerBase
{
}
