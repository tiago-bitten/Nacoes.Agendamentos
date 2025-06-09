using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Infra.Contexts;
using Nacoes.Agendamentos.ReadModels.Abstracts;
using Nacoes.Agendamentos.ReadModels.Extensions;
using AgendaId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Agendas.Agenda>;

namespace Nacoes.Agendamentos.ReadModels.Entities.Agendas.Queries.RecuperarAgendamento;

public sealed class RecuperarAgendamentoQuery(NacoesDbContext dbContext)
    : BaseQuery(dbContext), IRecuperarAgendamentoQuery
{
    public async Task<RecuperarAgendamentoResponse> ExecutarAsync(RecuperarAgendamentoParam param, AgendaId agendaId, CancellationToken cancellation = default)
    {
        throw new NotImplementedException();

        var queryAgendamentos = DbContext.Agendamentos.AsNoTracking();

        //queryAgendamentos = (from ag in DbContext.Agendas.AsNoTracking()
        //                     where ag.Id == agendaId
        //                      && ag.Agendamentos.Any(x => )

        queryAgendamentos = queryAgendamentos.PaginarPorKeyset(param.Take, param.UltimoId, param.UltimaDataCriacao);
    }
}
