using Application.Common.Enums;

namespace Application.Common.Settings;

public sealed record NotificationsSettings
{
    public required EmailSettings Email { get; init; }
}

public sealed record EmailSettings
{
    public required string Provider { get; init; }
    public SendGridEmailSettings? SendGrid { get; init; }
    public MailtrapEmailSettings? Mailtrap { get; init; }

    public EEmailProvider GetProvider => Enum.Parse<EEmailProvider>(Provider);
}

public sealed record SendGridEmailSettings
{
    public required string ApiKey { get; init; }
}

public sealed record MailtrapEmailSettings
{
    public required string ApiKey { get; init; }
}
