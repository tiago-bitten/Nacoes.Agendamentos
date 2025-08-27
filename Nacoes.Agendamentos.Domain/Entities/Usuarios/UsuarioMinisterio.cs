using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;

namespace Nacoes.Agendamentos.Domain.Entities.Usuarios;

public sealed class UsuarioMinisterio : EntityId
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