using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Domain.Entities.Agendas;

public static class AgendaErrors
{
    public static readonly Error DescricaoObrigatoria = 
        Error.Problem("Agenda.DescricaoObrigatoria", "A descrição da agenda deve ser informada."); 
}