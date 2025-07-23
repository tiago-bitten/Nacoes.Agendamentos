using Nacoes.Agendamentos.Domain.Entities.Voluntarios;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Interfaces;
using Nacoes.Agendamentos.Infra.Abstracts;
using Nacoes.Agendamentos.Infra.Contexts;

namespace Nacoes.Agendamentos.Infra.Entities.Voluntarios;

internal sealed class VoluntarioMinisterioRepository(NacoesDbContext dbContext)
    : BaseRepository<VoluntarioMinisterio>(dbContext), IVoluntarioMinisterioRepository
{
}