using Application.Common.Enums;

namespace Application.Common.Settings;

public sealed record EnvironmentSettings
{
    public required string Type { get; init; }

    public EEnvironment GetTypeEnum() => Enum.Parse<EEnvironment>(Type);
}
