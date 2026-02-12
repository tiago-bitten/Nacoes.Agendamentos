using Application.Shared.Contexts;
using Application.Shared.Messaging;
using Domain.Shared.Results;

namespace Application.Entities.Eventos.Commands.Reservas.AdicionarReserva;

internal sealed class AdicionarReservaHandler(
    INacoesDbContext context)
    : ICommandHandler<AdicionarReservaCommand, Guid>
{
    public async Task<Result<Guid>> HandleAsync(AdicionarReservaCommand command, CancellationToken cancellation = default)
    {
        throw new NotImplementedException();
    }
}
