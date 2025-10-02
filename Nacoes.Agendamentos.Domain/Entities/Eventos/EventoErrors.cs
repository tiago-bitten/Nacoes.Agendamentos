using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Domain.Entities.Eventos;

public static class EventoErrors
{
    public static readonly Error DescricaoObrigatoria = 
        Error.Problem("Evento.DescricaoObrigatoria", "A descrição do evento deve ser informada."); 
    
    public static readonly Error AgendamentoExistente = 
        Error.Problem("Evento.AgendamentoExistente", "Já existe um agendamento para esse voluntário no evento.");
    
    public static readonly Error NaoEstaDisponivelParaCancelarAgendamento = 
        Error.Problem("Evento.EventoNaoEstaDisponivelParaCancelarAgendamento", "O evento não está disponível para cancelar o agendamento.");
    
    public static readonly Error NaoEstaDisponivelParaAbrir = 
        Error.Problem("Evento.EventoNaoEstaDisponivelParaAbrir", "O evento não está disponível para abrir.");
    
    public static readonly Error NaoEstaDisponivelParaSuspender = 
        Error.Problem("Evento.EventoNaoEstaDisponivelParaSuspender", "O evento não está disponível para suspender.");
    
    public static readonly Error NaoEstaDisponivelParaCancelar = 
        Error.Problem("Evento.EventoNaoEstaDisponivelParaCancelar", "O evento não está disponível para cancelar.");
    
    public static readonly Error NaoEstaDisponivelParaAtualizarHorario = 
        Error.Problem("Evento.EventoNaoEstaDisponivelParaAtualizarHorario", "O evento não está disponível para atualizar o horário.");
    
    public static readonly Error NaoEstaDisponivelParaAtualizarRecorrencia = 
        Error.Problem("Evento.EventoNaoEstaDisponivelParaAtualizarRecorrencia", "O evento não está disponível para atualizar a recorrência.");
    
    public static readonly Error QuantidadeMaximaReservasInvalida = 
        Error.Problem("Evento.QuantidadeMaximaReservasInvalida", "A quantidade máxima de reservas deve ser maior que zero.");
}