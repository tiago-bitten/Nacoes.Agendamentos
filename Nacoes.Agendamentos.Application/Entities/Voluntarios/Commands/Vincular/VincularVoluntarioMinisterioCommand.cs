using Nacoes.Agendamentos.Application.Abstracts.Messaging;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.Vincular;
public record VincularVoluntarioMinisterioCommand : ICommand
{
    public Guid VoluntarioId { get; set; }
    public Guid MinisterioId { get; set; }
}
