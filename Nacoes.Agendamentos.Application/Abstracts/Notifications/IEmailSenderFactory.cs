namespace Nacoes.Agendamentos.Application.Abstracts.Notifications;

public interface IEmailSenderFactory
{
    IEmailSender Create();
}