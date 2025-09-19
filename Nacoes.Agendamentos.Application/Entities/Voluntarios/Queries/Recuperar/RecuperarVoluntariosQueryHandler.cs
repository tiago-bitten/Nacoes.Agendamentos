using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Common.Pagination;
using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Queries.Recuperar;

internal sealed class RecuperarVoluntariosQueryHandler(INacoesDbContext context)
    : IQueryHandler<RecuperarVoluntariosQuery, PagedResponse<VoluntarioResponse>>
{
    public async Task<Result<PagedResponse<VoluntarioResponse>>> Handle(RecuperarVoluntariosQuery query, CancellationToken cancellationToken = default)
    {
        var voluntarios = await context.Voluntarios
            .Select(x => new VoluntarioResponse
            { 
                Id = x.Id, 
                DataCriacao = x.DataCriacao, 
                Nome = x.Nome,
                Ministerios = x.Ministerios.Select(m => new VoluntarioResponse.MinisterioItem 
                {
                    Nome = m.Ministerio.Nome 
                }).ToList()
            }).ToPagedResponseAsync(query.Limit, query.Cursor);

        return voluntarios;
    }
}