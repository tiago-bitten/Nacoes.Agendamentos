using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Common.Pagination;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Interfaces;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Queries.Recuperar;

internal sealed class RecuperarVoluntariosQueryHandler(IVoluntarioRepository voluntarioRepository)
    : IQueryHandler<RecuperarVoluntariosQuery, PagedResponse<VoluntarioResponse>>
{
    public async Task<Result<PagedResponse<VoluntarioResponse>>> Handle(RecuperarVoluntariosQuery query, CancellationToken cancellationToken = default)
    {
        var voluntarios = await voluntarioRepository.GetAll(includes: "Ministerios.Ministerio")
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

        return Result<PagedResponse<VoluntarioResponse>>.Success(voluntarios);
    }
}