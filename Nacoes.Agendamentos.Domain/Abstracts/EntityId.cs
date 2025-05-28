using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Domain.Abstracts;

public abstract class EntityId<T> where T : class
{
    #region Ctor
    protected EntityId()
    {
        Id = Id<T>.Novo();
        DataCriacao = DateTime.UtcNow;
        Inativo = false;
    }
    #endregion

    public Id<T> Id { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public bool Inativo { get; private set; }

    public void Inativar() => Inativo = true;
}
