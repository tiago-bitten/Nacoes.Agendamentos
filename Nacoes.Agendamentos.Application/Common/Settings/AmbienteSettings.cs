using Nacoes.Agendamentos.Application.Common.Enums;

namespace Nacoes.Agendamentos.Application.Common.Settings;

public sealed record AmbienteSettings
{
    public required string Tipo { get; init; }
    public EEnvironment TipoEnum => Enum.Parse<EEnvironment>(Tipo);
}