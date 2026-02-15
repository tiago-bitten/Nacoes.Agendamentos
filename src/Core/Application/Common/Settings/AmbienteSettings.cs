using Application.Common.Enums;

namespace Application.Common.Settings;

public sealed record AmbienteSettings
{
    public required string Tipo { get; init; }

    public EEnvironment GetTipoEnum() => Enum.Parse<EEnvironment>(Tipo);
}
