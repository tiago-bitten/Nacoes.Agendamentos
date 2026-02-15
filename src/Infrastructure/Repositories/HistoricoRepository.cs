using Microsoft.EntityFrameworkCore;
using Application.Shared.Contexts;
using Domain.Historicos;
using Domain.Historicos.Interfaces;

namespace Infrastructure.Repositories;

internal sealed class AuditLogRepository(INacoesDbContext context) : IAuditLogRepository
{
    private DbSet<AuditLog> AuditLogs => context.AuditLogs;

    public async Task AddAsync(AuditLog auditLog, CancellationToken ct = default)
    {
        await AuditLogs.AddAsync(auditLog, ct);
    }

    public Task<List<AuditLog>> GetByEntityIdAsync(Guid entityId, CancellationToken ct = default)
    {
        return AuditLogs.Where(h => h.EntityId == entityId).ToListAsync(ct);
    }
}
