using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Common.Dtos;

public sealed record RecorrenciaEventoDto(ETipoRecorrenciaEvento Tipo, int? Valor, DateOnly? DataFinal);
