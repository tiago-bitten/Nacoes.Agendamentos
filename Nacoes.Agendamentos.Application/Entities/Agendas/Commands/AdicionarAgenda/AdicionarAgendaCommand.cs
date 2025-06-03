namespace Nacoes.Agendamentos.Application.Entities.Agendas.Commands.AdicionarAgenda;

public record AdicionarAgendaCommand
{
    public string Descricao { get; set; } = string.Empty;
    public HorarioItem Horario { get; set; } = new();

    public record HorarioItem
    {
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
    }
}
