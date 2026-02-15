using Domain.Shared.Entities;
using Domain.Enums;
using Domain.Shared.Results;

namespace Domain.Historicos;

public sealed class AuditLog : RemovableEntity
{
    public const int ActionMaxLength = 200;
    public const int DetailsMaxLength = 2000;

    private AuditLog() { }

    private AuditLog(
        Guid? entityId,
        DateTimeOffset date,
        Guid? userId,
        string action,
        EUserContextType userAction,
        string? details)
    {
        EntityId = entityId;
        Date = date;
        UserId = userId;
        Action = action;
        UserAction = userAction;
        Details = details;
    }

    public Guid? EntityId { get; private set; }
    public DateTimeOffset Date { get; private set; }
    public Guid? UserId { get; private set; }
    public string Action { get; private set; } = string.Empty;
    public EUserContextType UserAction { get; private set; }
    public string? Details { get; private set; }

    public static Result<AuditLog> Create(
        Guid? entityId,
        Guid? userId,
        string action,
        EUserContextType userAction,
        string? details)
    {
        action = action.Trim();
        details = details?.Trim();

        if (string.IsNullOrEmpty(action))
        {
            return AuditLogErrors.ActionRequired;
        }

        return new AuditLog(entityId, DateTimeOffset.UtcNow, userId, action, userAction, details);
    }
}
