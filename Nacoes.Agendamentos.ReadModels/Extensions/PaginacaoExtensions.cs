using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.ReadModels.Extensions;

public static class PaginacaoExtensions
{
    #region PaginarPorKeyset
    public static IQueryable<T> PaginarPorKeyset<T>(this IQueryable<T> query, int take, string? ultimoId, DateTime? ultimaDataCriacao)
        where T : EntityId<T>
    {
        if (ultimaDataCriacao.HasValue && !string.IsNullOrEmpty(ultimoId))
        {
            query = query.Where(x => x.DataCriacao < ultimaDataCriacao.Value ||
                                    (x.DataCriacao == ultimaDataCriacao.Value && x.Id.CompareTo(ultimoId) < 0));
        }

        return query.OrderByDescending(x => x.DataCriacao)
                    .ThenByDescending(x => x.Id)
                    .Take(take);
    }
    #endregion
}
