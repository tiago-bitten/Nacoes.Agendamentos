using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Errors;

namespace Nacoes.Agendamentos.Domain.Entities.Voluntarios;

public sealed class VoluntarioMinisterio : EntityId
{
    private VoluntarioMinisterio() { }

    internal VoluntarioMinisterio(Guid ministerioId)
    {
        MinisterioId = ministerioId;
    }

    public Guid MinisterioId { get; private set; }
    
    public Ministerio Ministerio { get; private set; } = null!;
    public Voluntario Voluntario { get; private set; } = null!;
    
}