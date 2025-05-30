namespace Nacoes.Agendamentos.Application.Common.Results;

public sealed record Error(string Code, string? Message = null);
