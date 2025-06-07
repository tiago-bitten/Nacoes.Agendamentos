using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.Exceptions;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Domain.Entities.Usuarios;

public sealed class UsuarioAprovacaoMinisterio : EntityId<UsuarioAprovacaoMinisterio>
{
    #region Construtores
    internal UsuarioAprovacaoMinisterio() { }

    internal UsuarioAprovacaoMinisterio(Id<Ministerio> ministerioId)
    {
        MinisterioId = ministerioId;
        Aprovado = false;
    }

    #endregion

    public Id<Ministerio> MinisterioId { get; private set; }
    public bool Aprovado { get; private set; }

    internal void Aprovar()
    {
        if (Aprovado)
        {
            throw ExceptionFactory.MinisterioSolicitanteJaFoiAprovado();
        }

        Aprovado = true;
    }
}
