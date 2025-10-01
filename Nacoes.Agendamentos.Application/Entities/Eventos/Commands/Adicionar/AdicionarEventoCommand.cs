using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Common.Dtos;

namespace Nacoes.Agendamentos.Application.Entities.Eventos.Commands.Adicionar;

public sealed record AdicionarEventoCommand(
    string Descricao,
    HorarioDto Horario,
    RecorrenciaEventoDto Recorrencia) : ICommand<Guid>;