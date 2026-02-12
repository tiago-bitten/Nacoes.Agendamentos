using Domain.Shared.Entities;
using Domain.Shared.Results;
using Domain.Ministerios;

namespace Domain.Usuarios;

public sealed class UsuarioConviteMinisterio : RemovableEntity
{
    private UsuarioConviteMinisterio() { }

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
