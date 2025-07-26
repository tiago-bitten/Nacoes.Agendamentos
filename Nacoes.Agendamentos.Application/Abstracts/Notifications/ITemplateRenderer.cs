namespace Nacoes.Agendamentos.Application.Abstracts.Notifications;

public interface ITemplateRenderer
{
    string Render(string templateName, Dictionary<string, string> placeholders);
}