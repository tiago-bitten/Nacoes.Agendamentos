using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Domain.Entities.Usuarios;

public sealed class UsuarioAprovacao : EntityId<UsuarioAprovacao>
{
    #region Construtor
    internal UsuarioAprovacao()
    {
        Status = EStatusAprovacao.Aguardando;
    }
    #endregion

    public Usuario? Aprovador { get; private set; }
    public EStatusAprovacao Status { get; private set; }
    public DateTime? DataAprovacao { get; private set; }

    private IList<UsuarioAprovacaoMinisterio> _ministerios = [];
    public IReadOnlyCollection<UsuarioAprovacaoMinisterio> Ministerios => _ministerios.AsReadOnly();

    public bool PodeSolicitar => Status == EStatusAprovacao.Reprovado;
    public bool FoiAvaliado => Status != EStatusAprovacao.Aguardando && DataAprovacao != null && Aprovador != null;

    #region Aprovar
    internal void Aprovar(Usuario aprovador, IList<Id<Ministerio>> ministeriosAprovados)
    {
        if (FoiAvaliado)
        {
            throw new Exception("O usuário já foi avaliado");
        }

        Status = EStatusAprovacao.Aprovado;
        Aprovador = aprovador;
        DataAprovacao = DateTime.UtcNow;

        foreach (var ministerio in _ministerios)
        {
            if (ministeriosAprovados.Contains(ministerio.MinisterioId))
            {
                ministerio.Aprovar();
            }
        }
    }
    #endregion

    #region Reprovar
    internal void Reprovar(Usuario aprovador)
    {
        if (FoiAvaliado)
        {
            throw new Exception("O usuário já foi avaliado");
        }

        Status = EStatusAprovacao.Reprovado;
        Aprovador = aprovador;
        DataAprovacao = DateTime.UtcNow;
    }
    #endregion

    #region AdicionarMinisterios
    internal void AdicionarMinisterios(IList<Id<Ministerio>> ministerios)
    {
        foreach (var ministerio in ministerios)
        {
            if (_ministerios.Any(m => m.MinisterioId == ministerio))
            {
                continue;
            }

            _ministerios.Add(new UsuarioAprovacaoMinisterio(ministerio));
        }
    }
    #endregion
}

public enum EStatusAprovacao
{
    Aguardando = 1,
    Aprovado = 2,
    Reprovado = 3
}