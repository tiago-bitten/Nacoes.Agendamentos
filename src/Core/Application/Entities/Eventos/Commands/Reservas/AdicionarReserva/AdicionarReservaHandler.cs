using Application.Shared.Contexts;
using Application.Shared.Messaging;
using Domain.Shared.Results;

namespace Application.Entities.Eventos.Commands.Reservas.AdicionarReserva;

internal sealed class AddReservationHandler(
    INacoesDbContext context)
    : ICommandHandler<AddReservationCommand, Guid>
{
    public async Task<Result<Guid>> HandleAsync(
        AddReservationCommand command,
        CancellationToken ct)
    {
        return Result.Failure<Guid>(Error.Problem("reservation.notimplemented", "Feature not implemented."));
    }
}
