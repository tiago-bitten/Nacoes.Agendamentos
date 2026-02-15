using Domain.Shared.ValueObjects;

namespace Application.Shared.Ports.Notifications;

public interface IEmailSender
{
    Task SendAsync(Email to, string subject, string body, CancellationToken ct = default);
}
