namespace Domain.Historicos.Interfaces;

public interface IAuditLogRepository
{
    Task AddAsync(AuditLog auditLog, CancellationToken ct = default);
    Task<List<AuditLog>> GetByEntityIdAsync(Guid entityId, CancellationToken ct = default);
}
