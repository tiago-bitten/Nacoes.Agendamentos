using Nacoes.Agendamentos.Application.Abstracts.Notifications;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Infra.Notifications.Emails;

public sealed class SendGridEmailSender : IEmailSender
{
    public Task SendAsync(Email to, string subject, string body)
    {
        throw new NotImplementedException();
    }
}