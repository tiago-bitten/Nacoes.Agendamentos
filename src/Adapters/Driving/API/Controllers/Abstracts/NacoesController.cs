using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Application.Common.Responses;
using Domain.Shared.Results;

namespace API.Controllers.Abstracts;

[Route("api/[controller]")]
[ApiController]
public abstract class NacoesController : ControllerBase
{
}
