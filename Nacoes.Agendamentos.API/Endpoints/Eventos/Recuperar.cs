using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.API.Extensions;
using Nacoes.Agendamentos.API.Infra;
using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Common.Dtos;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Eventos;

namespace Nacoes.Agendamentos.API.Endpoints.Eventos;

internal sealed class Recuperar : IEndpoint
{
    public sealed record Request(DateOnly DataInicial, DateOnly DataFinal);

    public sealed record Response
    {
        public Guid Id { get; init; }
        public string Descricao { get; init; } = string.Empty;
        public int QuantidadeReservas { get; init; }
        public int? QuantidadeMaximaReservas { get; init; }
        public HorarioDto Horario { get; init; } = null!;
        public EStatusEvento Status { get; init; }
    }
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/eventos", async (
            [AsParameters] Request request,
            [FromServices] INacoesDbContext context,
            CancellationToken cancellationToken) =>
        {
            Result<List<Response>> result;
            
            try
            {
                var eventos = await context.Eventos
                    .Where(x => x.Status != EStatusEvento.Cancelado &&
                                DateOnly.FromDateTime(x.Horario.DataInicial.DateTime) >= request.DataInicial &&
                                DateOnly.FromDateTime(x.Horario.DataFinal.DateTime) <= request.DataFinal)
                    .Select(x => new Response
                    {
                        Id = x.Id,
                        Descricao = x.Descricao,
                        QuantidadeReservas = x.QuantidadeReservas,
                        QuantidadeMaximaReservas = x.QuantidadeMaximaReservas,
                        Horario = x.Horario.ToDto(),
                        Status = x.Status
                    })
                    .ToListAsync(cancellationToken);
                
                result = Result<List<Response>>.Success(eventos);
            }
            catch (Exception ex)
            {
                var error = Error.Problem("RecuperarEventos", ex.Message);
                result = Result<List<Response>>.Failure(error);
            }
            
            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Eventos);
    }
}