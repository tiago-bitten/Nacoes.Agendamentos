using Application.Shared.Contexts;
using Application.Shared.Messaging;
using Application.Shared.Pagination;
using Domain.Shared.Results;

namespace Application.Entities.Voluntarios.Queries.Recuperar;

internal sealed class RecuperarVoluntariosQueryHandler(INacoesDbContext context)
    : IQueryHandler<RecuperarVoluntariosQuery, PagedResponse<VoluntarioResponse>>
{
    public async Task<Result<PagedResponse<VoluntarioResponse>>> Handle(RecuperarVoluntariosQuery query, CancellationToken cancellationToken = default)
    {
        var voluntarios = await context.Voluntarios
            .Select(x => new VoluntarioResponse
            {
                Id = x.Id,
                CreatedAt = x.CreatedAt,
                Nome = x.Nome,
                Ministerios = x.Ministerios.Select(m => new VoluntarioResponse.MinisterioItem
                {
                    Nome = m.Ministerio.Nome
                }).ToList()
            }).ToPagedResponseAsync(query.Limit, query.Cursor, cancellationToken);

        return voluntarios;
    }
}
