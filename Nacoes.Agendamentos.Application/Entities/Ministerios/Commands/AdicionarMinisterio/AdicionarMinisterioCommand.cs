using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Entities.Ministerios.Commands.AdicionarMinisterio;
public record AdicionarMinisterioCommand
{
    public string Nome { get; set; }
    public string? Descricao { get; set; }
    public CorItem? Cor { get; set; }

    public record CorItem
    {
        public string Valor { get; set; }
        public ETipoCor Tipo { get; set; }
    }
}
