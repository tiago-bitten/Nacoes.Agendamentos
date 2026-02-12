using Application.Shared.Messaging;

namespace Application.Entities.Voluntarios.Commands.Desvincular;

public sealed record DesvincularVoluntarioMinisterioCommand(Guid VoluntarioMinisterioId) : ICommand;
