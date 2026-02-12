using Domain.Shared.Entities;
using Domain.Ministerios;

namespace Domain.Usuarios;

public sealed class UsuarioMinisterio : RemovableEntity
{
    private UsuarioMinisterio() { }

    internal UsuarioMinisterio(Guid ministerioId)
    {
        MinisterioId = ministerioId;
    }

    public Guid UsuarioId { get; private set; }
    public Guid MinisterioId { get; private set; }

    public Usuario Usuario { get; private set; } = null!;
    public Ministerio Ministerio { get; private set; } = null!;
}
