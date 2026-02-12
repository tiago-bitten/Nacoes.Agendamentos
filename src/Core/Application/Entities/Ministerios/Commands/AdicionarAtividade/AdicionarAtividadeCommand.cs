using Application.Shared.Messaging;

namespace Application.Entities.Ministerios.Commands.AdicionarAtividade;

public sealed record AdicionarAtividadeCommand(string Nome, string? Descricao, Guid MinisterioId) : ICommand<Guid>;
