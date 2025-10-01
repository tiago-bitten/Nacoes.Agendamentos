using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Domain.Entities.Eventos.Suspensoes;

public static class EventoSuspensaoErrors
{
    public static readonly Error DataFinalNaoPodeSerAnterioraDataHoje =
        Error.Problem("eventos.suspensoes.dataFinalNaoPodeSerAnterioraDataHoje", "A data final da suspenção não pode ser anterior à data de hoje.");
    
    public static readonly Error DataFinalNaoPodeSerHoje =
        Error.Problem("eventos.suspensoes.dataFinalNaoPodeSerHoje", "A data final da suspenção não pode ser hoje.");
    
    public static readonly Error NaoEstaDisponivelParaEncerrar =
        Error.Problem("eventos.suspensoes.naoEstaDisponivelParaEncerrar", "A suspenção não está disponível para encerramento.");
    
    public static readonly Error NaoEstaDisponivelParaCancelar =
        Error.Problem("eventos.suspensoes.naoEstaDisponivelParaCancelar", "A suspenção já foi cancelada.");
}