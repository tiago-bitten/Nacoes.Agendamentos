namespace Application.Shared.Ports.Notifications;

public interface ITemplateRenderer
{
    string Render(string templateName, Dictionary<string, string> placeholders);
}
