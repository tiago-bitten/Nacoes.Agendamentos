using Application.Shared.Messaging;

namespace Application.Entities.Voluntarios.Commands.Vincular;

public sealed record VincularVoluntarioMinisterioCommand(Guid VoluntarioId, Guid MinisterioId) : ICommand;
