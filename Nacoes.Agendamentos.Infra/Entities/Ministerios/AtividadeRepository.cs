using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.Entities.Ministerios.Interfaces;
using Nacoes.Agendamentos.Infra.Abstracts;
using Nacoes.Agendamentos.Infra.Contexts;

namespace Nacoes.Agendamentos.Infra.Entities.Ministerios;

internal sealed class AtividadeRepository(NacoesDbContext dbContext)
    : BaseRepository<Atividade>(dbContext), IAtividadeRepository
{
}