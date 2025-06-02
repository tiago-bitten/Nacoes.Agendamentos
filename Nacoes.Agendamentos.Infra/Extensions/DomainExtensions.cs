using Nacoes.Agendamentos.Domain.Abstracts;

namespace Nacoes.Agendamentos.Infra.Extensions;

public static class DomainExtensions
{
    public static bool NotPersisted<T>(this T entity) where T : EntityId<T>
        => entity.Id?.Value.Equals(Guid.Empty) ?? true;
}
