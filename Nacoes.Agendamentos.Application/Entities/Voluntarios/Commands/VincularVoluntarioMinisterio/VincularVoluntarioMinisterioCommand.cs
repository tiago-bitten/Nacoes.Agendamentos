namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.VincularVoluntarioMinisterio;
public record VincularVoluntarioMinisterioCommand
{
    public Guid VoluntarioId { get; set; }
    public Guid MinisterioId { get; set; }
}
