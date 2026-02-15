using Microsoft.Extensions.Options;
using Application.Shared.Ports.Notifications;
using Application.Common.Enums;
using Application.Common.Settings;

namespace Infrastructure.Notifications.Emails;

internal sealed class EmailSenderFactory(IOptions<NotificationsSettings> notificationsSettings)
    : IEmailSenderFactory
{
    private EEmailProvider Provider => notificationsSettings.Value.Email.GetProvider;

    public IEmailSender Create()
        => Provider switch
        {
            EEmailProvider.SendGrid => new SendGridEmailSender(),
            _ => throw new ArgumentOutOfRangeException()
        };
}
