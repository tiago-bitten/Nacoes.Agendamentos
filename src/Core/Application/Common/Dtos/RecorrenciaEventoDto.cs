using Domain.Shared.ValueObjects;

namespace Application.Common.Dtos;

public sealed record RecorrenciaEventoDto(ETipoRecorrenciaEvento Tipo, int? Intervalo, DateOnly? DataFinal);
