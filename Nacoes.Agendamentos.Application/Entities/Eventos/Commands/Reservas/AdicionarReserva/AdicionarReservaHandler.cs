using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Application.Entities.Eventos.Commands.Reservas.AdicionarReserva;

internal sealed class AdicionarReservaHandler(
    INacoesDbContext context)
    : ICommandHandler<AdicionarReservaCommand, Guid>
{
    public async Task<Result<Guid>> HandleAsync(AdicionarReservaCommand command, CancellationToken cancellation = default)
    {
        throw new NotImplementedException();
    }
}