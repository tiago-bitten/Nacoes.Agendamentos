using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Common.Dtos;

namespace Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.Adicionar;

public sealed record AdicionarMinisterioCommand(string Nome, string? Descricao, CorDto Cor) : ICommand<Guid>;
