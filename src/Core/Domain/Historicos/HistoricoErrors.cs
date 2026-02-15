using Domain.Shared.Results;

namespace Domain.Historicos;

public static class AuditLogErrors
{
    public static readonly Error ActionRequired =
        Error.Validation("audit_log.action_required", "The action is required.");
}
