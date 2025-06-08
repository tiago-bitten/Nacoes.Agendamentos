namespace Nacoes.Agendamentos.Application.Entities.Agendas.Commands.Agendar;

public record AgendarCommand
{
    public Guid VoluntarioMinisterioId { get; init; }
    public Guid AtividadeId { get; init; }
}
