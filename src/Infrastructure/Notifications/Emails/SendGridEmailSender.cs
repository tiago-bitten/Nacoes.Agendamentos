using Application.Shared.Ports.Notifications;
using Domain.Shared.ValueObjects;

namespace Infrastructure.Notifications.Emails;

internal sealed class SendGridEmailSender : IEmailSender
{
    public Task SendAsync(Email to, string subject, string body, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
