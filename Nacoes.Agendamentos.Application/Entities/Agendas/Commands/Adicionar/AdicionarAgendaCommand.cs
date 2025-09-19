using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Common.Dtos;

namespace Nacoes.Agendamentos.Application.Entities.Agendas.Commands.Adicionar;

public sealed record AdicionarAgendaCommand(string Descricao, HorarioDto Horario) : ICommand<Guid>;