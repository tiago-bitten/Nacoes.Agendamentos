using Nacoes.Agendamentos.Application.Abstracts.Messaging;

namespace Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.AdicionarAtividade;

public sealed record AdicionarAtividadeCommand(string Nome, string? Descricao, Guid MinisterioId) : ICommand<Guid>;
