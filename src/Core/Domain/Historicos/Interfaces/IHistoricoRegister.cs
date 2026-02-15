using Domain.Shared.Entities;

namespace Domain.Historicos.Interfaces;

public interface IAuditLogRegister
{
    Task AuditAsync(Guid entityId, string action, string? details = null, CancellationToken ct = default);
    Task AuditAsync<T>(T entity, string action, string? details = null, CancellationToken ct = default) where T : Entity;
}
