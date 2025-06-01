namespace Nacoes.Agendamentos.Application.Common.Results;

public sealed record Error(string Codigo, string? Mensagem = null);