using Domain.Shared.Results;

namespace Domain.Eventos.Reservas;

public static class ReservaErrors
{
    public static readonly Error NaoEstaReservado =
        Error.Problem("Reserva", "Apenas é possível cancelar reservas com a situação 'Confirmado' ou 'AguardandoConfirmacao'.");

    public static readonly Error NaoEncontrado =
        Error.Problem("Reserva", "Não foi possível encontrar a reserva.");
}
