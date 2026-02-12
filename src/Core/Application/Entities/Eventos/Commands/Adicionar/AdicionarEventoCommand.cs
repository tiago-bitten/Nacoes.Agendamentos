using Application.Shared.Messaging;
using Application.Common.Dtos;

namespace Application.Entities.Eventos.Commands.Adicionar;

public sealed record AdicionarEventoCommand(
    string Descricao,
    HorarioDto Horario,
    RecorrenciaEventoDto Recorrencia,
    int? QuantidadeMaximaReservas) : ICommand<Guid>;
