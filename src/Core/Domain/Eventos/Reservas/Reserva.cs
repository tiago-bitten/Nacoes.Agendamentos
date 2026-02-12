using Domain.Shared.Entities;
using Domain.Shared.Results;

namespace Domain.Eventos.Reservas;

public sealed class Reserva : RemovableEntity
{
    private Reserva()
    {
    }

    private Reserva(
        Guid voluntarioMinisterioId,
        Guid atividadeId,
        EStatusReserva status,
        EOrigemReserva origem)
    {
        VoluntarioMinisterioId = voluntarioMinisterioId;
        AtividadeId = atividadeId;
        Status = status;
        Origem = origem;
    }

    public Guid EventoId { get; private set; }
    public Guid VoluntarioMinisterioId { get; private set; }
    public Guid AtividadeId { get; private set; }
    public EStatusReserva Status { get; private set; }
    public EOrigemReserva Origem { get; private set; }

    public Evento Evento { get; private set; } = null!;

    internal static Result<Reserva> Criar(Guid voluntarioMinisterioId, Guid atividadeId, EOrigemReserva origem)
    {
        return new Reserva(voluntarioMinisterioId, atividadeId, EStatusReserva.Confirmado, origem);
    }

    internal Result Cancelar()
    {
        if (Status is not (EStatusReserva.Confirmado or EStatusReserva.AguardandoConfirmacao))
        {
            return ReservaErrors.NaoEstaReservado;
        }

        Status = EStatusReserva.Cancelado;

        return Result.Success();
    }
}

public enum EStatusReserva
{
    AguardandoConfirmacao = 0,
    Confirmado = 1,
    Cancelado = 2
}

public enum EOrigemReserva
{
    Manual = 0,
    Automatico = 1,
    Escala = 2
}
