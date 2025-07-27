using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Interfaces;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Queries.Recuperar;

internal sealed class RecuperarVoluntariosQueryHandler(IVoluntarioRepository voluntarioRepository)
    : IQueryHandler<RecuperarVoluntariosQuery, List<VoluntarioResponse>>
{
    public async Task<Result<List<VoluntarioResponse>>> Handle(RecuperarVoluntariosQuery query, CancellationToken cancellationToken = default)
    {
        var voluntarios = await voluntarioRepository.GetAll(includes: "Ministerios.Ministerio")
                                                    .Select(x => new VoluntarioResponse
                                                    {
                                                        Id = x.Id,
                                                        Nome = x.Nome,
                                                        Ministerios = x.Ministerios.Select(m => new VoluntarioResponse.MinisterioItem
                                                        {
                                                            Nome = m.Ministerio.Nome
                                                        }).ToList()
                                                    }).ToListAsync(cancellationToken);

        return Result<List<VoluntarioResponse>>.Success(voluntarios);
    }
}