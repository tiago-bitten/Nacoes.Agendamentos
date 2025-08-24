using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;

namespace Nacoes.Agendamentos.Domain.Entities.Usuarios;

public sealed class UsuarioConviteMinisterio : EntityId
{
    private UsuarioConviteMinisterio() {}
    
    private UsuarioConviteMinisterio(Guid conviteId, Guid ministerioId)
    {
        ConviteId = conviteId;
        MinisterioId = ministerioId;
    }
    
    public Guid ConviteId { get; private set; }
    public Guid MinisterioId { get; private set; }
    
    public UsuarioConvite Convite { get; private set; } = null!;
    public Ministerio Ministerio { get; private set; } = null!;
    
    public static Result<UsuarioConviteMinisterio> Criar(Guid conviteId, Guid ministerioId)
    {
        if (conviteId == Guid.Empty)
        {
            return UsuarioConviteMinisterioErrors.ConviteObrigatorio;
        }
        
        if (ministerioId == Guid.Empty)
        {
            return UsuarioConviteMinisterioErrors.MinisterioObrigatorio;
        }
        
        return new UsuarioConviteMinisterio(conviteId, ministerioId);
    }
}