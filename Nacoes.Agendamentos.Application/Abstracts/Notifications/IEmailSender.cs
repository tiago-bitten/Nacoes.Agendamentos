using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Abstracts.Notifications;

public interface IEmailSender
{
    Task SendAsync(Email to, string subject, string body);
}
