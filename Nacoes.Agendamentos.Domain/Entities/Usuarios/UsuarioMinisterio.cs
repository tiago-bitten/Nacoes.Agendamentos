using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;

namespace Nacoes.Agendamentos.Domain.Entities.Usuarios;

public sealed class UsuarioMinisterio : EntityId
{
    private UsuarioMinisterio() { }
    
    private UsuarioMinisterio(Guid usuarioId, Guid ministerioId)
    {
        UsuarioId = usuarioId;
        MinisterioId = ministerioId;
    }
    
    public Guid UsuarioId { get; private set; }
    public Guid MinisterioId { get; private set; }
    public EUsuarioMinisterioStatus Status { get; private set; } = EUsuarioMinisterioStatus.Vinculado;
    
    public Usuario Usuario { get; private set; } = null!;
    public Ministerio Ministerio { get; private set; } = null!;
    
    public static Result<UsuarioMinisterio> Criar(Guid usuarioId, Guid ministerioId)
    {
        if (usuarioId == Guid.Empty)
        {
            return UsuarioMinisterioErrors.UsuarioObrigatorio;
        }
        
        if (ministerioId == Guid.Empty)
        {
            return UsuarioMinisterioErrors.MinisterioObrigatorio;
        }
        
        return new UsuarioMinisterio(usuarioId, ministerioId);
    }
    
    public Result Vincular()
    {
        if (Status is EUsuarioMinisterioStatus.Vinculado)
        {
            return UsuarioMinisterioErrors.UsuarioJaVinculadoAoMinisterio;
        }

        Status = EUsuarioMinisterioStatus.Vinculado;
        
        return Result.Success();
    }
    
    public Result Desvincular()
    {
        if (Status is EUsuarioMinisterioStatus.Desvinculado)
        {
            return UsuarioMinisterioErrors.UsuarioJaDesvinculadoDoMinisterio;
        }

        Status = EUsuarioMinisterioStatus.Desvinculado;
        
        return Result.Success();
    }
}

public enum EUsuarioMinisterioStatus
{
    Vinculado = 0,
    Desvinculado = 1
}