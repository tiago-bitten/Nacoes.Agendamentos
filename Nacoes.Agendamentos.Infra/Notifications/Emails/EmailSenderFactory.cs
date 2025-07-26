using Microsoft.Extensions.Options;
using Nacoes.Agendamentos.Application.Abstracts.Notifications;
using Nacoes.Agendamentos.Application.Common.Enums;
using Nacoes.Agendamentos.Application.Common.Settings;

namespace Nacoes.Agendamentos.Infra.Notifications.Emails;

public sealed class EmailSenderFactory(IOptions<NotificationsSettings> notificationsSettings)
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