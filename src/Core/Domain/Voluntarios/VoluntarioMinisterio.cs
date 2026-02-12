using Domain.Shared.Entities;
using Domain.Ministerios;

namespace Domain.Voluntarios;

public sealed class VoluntarioMinisterio : RemovableEntity
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
