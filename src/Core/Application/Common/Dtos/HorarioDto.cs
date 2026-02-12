using Domain.Shared.ValueObjects;

namespace Application.Common.Dtos;

public record HorarioDto(DateTimeOffset DataInicial, DateTimeOffset DataFinal);

public static class HorarioDtoExtensions
{
    public static HorarioDto ToDto(this Horario horario) => new(horario.DataInicial, horario.DataFinal);
}
