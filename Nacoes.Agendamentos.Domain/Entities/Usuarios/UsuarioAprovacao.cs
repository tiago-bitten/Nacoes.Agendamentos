using Nacoes.Agendamentos.Domain.Abstracts;

namespace Nacoes.Agendamentos.Domain.Entities.Usuarios;

public sealed class UsuarioAprovacao : EntityId<UsuarioAprovacao>
{
    #region Construtor
    internal UsuarioAprovacao(Usuario solicitante)
    {
        Status = EStatusAprovacao.Aguardando;
        Solicitante = solicitante;
    }
    #endregion

    public Usuario? Aprovador { get; private set; }
    public Usuario Solicitante { get; private set; }
    public EStatusAprovacao Status { get; private set; }
    public DateTime? DataAprovacao { get; private set; }

    public bool PodeSolicitar => Status == EStatusAprovacao.Reprovado;
    public bool FoiAvaliado => Status != EStatusAprovacao.Aguardando && DataAprovacao != null && Aprovador != null;

    #region Aprovar
    public void Aprovar(Usuario aprovador)
    {
        if (FoiAvaliado)
        {
            throw new Exception("O usuário já foi avaliado");
        }

        if (aprovador.Equals(Solicitante))
        {
            throw new Exception("O usuário solicitante não pode aprovar");
        }

        Status = EStatusAprovacao.Aprovado;
        Aprovador = aprovador;
        DataAprovacao = DateTime.UtcNow;
    }
    #endregion

    #region Reprovar
    public void Reprovar(Usuario aprovador)
    {
        if (FoiAvaliado)
        {
            throw new Exception("O usuário já foi avaliado");
        }

        if (aprovador.Equals(Solicitante))
        {
            throw new Exception("O usuário solicitante não pode reprovar");
        }

        Status = EStatusAprovacao.Reprovado;
        Aprovador = aprovador;
        DataAprovacao = DateTime.UtcNow;
    }
    #endregion
}

public enum EStatusAprovacao
{
    Aguardando = 1,
    Aprovado = 2,
    Reprovado = 3
}