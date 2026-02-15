using Application.Shared.Messaging;

namespace Application.Entities.Voluntarios.Commands.Vincular;

public sealed record LinkVolunteerMinistryCommand(Guid VolunteerId, Guid MinistryId) : ICommand;
