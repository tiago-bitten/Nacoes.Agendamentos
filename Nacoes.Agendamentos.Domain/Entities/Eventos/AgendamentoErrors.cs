using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Domain.Entities.Eventos;

public static class AgendamentoErrors
{
    public static readonly Error NaoEstaAgendado = 
        Error.Problem("Agendamento", "Apenas é possível cancelar agendamentos com a situação 'Agendado'.");

    public static readonly Error NaoEncontrado =
        Error.Problem("Agendamento", "Não foi possível encontrar o agendamento.");
}