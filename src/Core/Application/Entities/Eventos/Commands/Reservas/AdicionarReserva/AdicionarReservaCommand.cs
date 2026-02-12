using Application.Shared.Messaging;

namespace Application.Entities.Eventos.Commands.Reservas.AdicionarReserva;

internal sealed record AdicionarReservaCommand : ICommand<Guid>
{
    public Guid EventoId { get; init; }
    public Guid VoluntarioMinisterioId { get; init; }
    public Guid AtividadeId { get; init; }
}
