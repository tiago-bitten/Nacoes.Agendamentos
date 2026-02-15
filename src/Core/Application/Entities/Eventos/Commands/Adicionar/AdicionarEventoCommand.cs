using Application.Shared.Messaging;
using Application.Common.Dtos;

namespace Application.Entities.Eventos.Commands.Adicionar;

public sealed record AddEventCommand(
    string Description,
    ScheduleDto Schedule,
    EventRecurrenceDto Recurrence,
    int? MaxReservationCount) : ICommand<Guid>;
