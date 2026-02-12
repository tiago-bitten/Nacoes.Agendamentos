using Application.Shared.Messaging;
using Application.Common.Dtos;

namespace Application.Entities.Ministerios.Commands.Adicionar;

public sealed record AdicionarMinisterioCommand(string Nome, string? Descricao, CorDto Cor) : ICommand<Guid>;
