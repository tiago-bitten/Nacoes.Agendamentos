namespace Application.Shared.Ports.Notifications;

public interface IEmailSenderFactory
{
    IEmailSender Create();
}
