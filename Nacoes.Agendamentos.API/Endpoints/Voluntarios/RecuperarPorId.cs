using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.API.Extensions;
using Nacoes.Agendamentos.API.Infra;
using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios;

namespace Nacoes.Agendamentos.API.Endpoints.Voluntarios;

internal sealed class RecuperarPorId : IEndpoint
{
    public sealed record Response
    {
        public Guid Id { get; init; }
        public string Nome { get; init; } = string.Empty;
        public string? Email { get; init; }
        public string? Celular { get; init; }
        public string? Cpf { get; init; }
        public DateOnly? DataNascimento { get; init; }
        public EOrigemCadastroVoluntario OrigemCadastro { get; init; }
        public List<MinisterioItem> Ministerios { get; init; } = [];
        
        public sealed record MinisterioItem
        {
            public Guid Id { get; init; }
            public string Nome { get; init; } = string.Empty;
            public string Cor { get; init; } = string.Empty;
        }
    }
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/voluntarios/{id:guid}", async (
            [FromServices] INacoesDbContext context,
            [FromRoute] Guid id,
            CancellationToken cancellationToken) =>
        {
            Result<Response> result;
            
            try
            {
                var voluntario = await context.Voluntarios
                    .AsNoTracking()
                    .Select(x => new Response
                    {
                        Id = x.Id,
                        Nome = x.Nome,
                        Email = x.EmailAddress,
                        Celular = x.Celular !.ToString(),
                        Cpf = x.Cpf != null ? x.Cpf.Numero : null,
                        DataNascimento = x.DataNascimento != null ? x.DataNascimento.Valor : null,
                        OrigemCadastro = x.OrigemCadastro,
                        Ministerios = x.Ministerios.Select(m => new Response.MinisterioItem
                        {
                            Id = m.Id,
                            Nome = m.Ministerio.Nome,
                            Cor = m.Ministerio.Cor.ToCssString()
                        }).ToList()
                    }).SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
                
                result = Result<Response>.Success(voluntario);
            }
            catch (Exception ex)
            {
                var error = Error.Problem("RecuperarVoluntarioPorId", ex.Message);
                result = Result<Response>.Failure(error);
            }
            
            return result.Match(Results.Ok, CustomResults.Problem);
        });
    }
}