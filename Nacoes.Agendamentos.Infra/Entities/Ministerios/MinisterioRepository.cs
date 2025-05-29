using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.Entities.Ministerios.Interfaces;
using Nacoes.Agendamentos.Infra.Abstracts;
using Nacoes.Agendamentos.Infra.Contexts;

namespace Nacoes.Agendamentos.Infra.Entities.Ministerios;

public class MinisterioRepository : BaseRepository<Ministerio>, IMinisterioRepository
{
    public MinisterioRepository(NacoesDbContext dbContext)
        : base(dbContext)
    {
    }
}
