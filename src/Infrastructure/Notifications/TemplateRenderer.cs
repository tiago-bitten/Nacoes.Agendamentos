using Application.Shared.Ports.Notifications;

namespace Infrastructure.Notifications;

internal sealed class TemplateRenderer : ITemplateRenderer
{
    private static readonly string AssemblyLocation = Path.GetDirectoryName(typeof(TemplateRenderer).Assembly.Location)!;
    private static readonly string BasePath = Path.Combine(AssemblyLocation, "Notifications", "Templates", "Emails");

    public string Render(string templateName, Dictionary<string, string> placeholders)
    {
        var path = Path.Combine(BasePath, $"{templateName}.html");
        var html = File.ReadAllText(path);

        foreach (var (key, value) in placeholders)
        {
            html = html.Replace($"{{{{{key}}}}}", value);
        }

        return html;
    }
}
