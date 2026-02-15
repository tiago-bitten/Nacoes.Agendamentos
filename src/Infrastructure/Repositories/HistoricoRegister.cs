using Application.Shared.Contexts;
using Application.Authentication.Context;
using Domain.Shared.Entities;
using Domain.Historicos;
using Domain.Historicos.Interfaces;
using Domain.Enums;
using Domain.Shared.Results;

namespace Infrastructure.Repositories;

internal sealed class AuditLogRegister(
    IEnvironmentContext environmentContext,
    INacoesDbContext context)
    : IAuditLogRegister
{
    public async Task AuditAsync(Guid entityId, string action, string? details, CancellationToken ct = default)
    {
        if (!environmentContext.IsUserAuthenticated)
        {
            throw new Exception("User not identified");
        }

        var userId = environmentContext.UserId;
        var userAction = environmentContext.UserContextType;

        var auditLogResult = AuditLog.Create(entityId, userId, action, userAction, details);
        if (auditLogResult.IsFailure)
        {
            return;
        }

        var auditLog = auditLogResult.Value;

        await context.AuditLogs.AddAsync(auditLog, ct);
        await context.SaveChangesAsync(ct);
    }

    public Task AuditAsync<T>(T entity, string action, string? details, CancellationToken ct = default) where T : Entity
    {
        return AuditAsync(entity.Id, action, details, ct);
    }
}
