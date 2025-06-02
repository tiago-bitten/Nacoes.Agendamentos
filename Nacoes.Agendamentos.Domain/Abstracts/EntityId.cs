using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Domain.Abstracts;

public abstract class EntityId<T> where T : class
{
    #region Construtor
    protected EntityId()
    {
        Id = new Id<T>(Guid.Empty.ToString());
        DataCriacao = DateTime.UtcNow;
        Inativo = false;
    }
    #endregion

    public Id<T> Id { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public bool Inativo { get; private set; }

    // USAR ISSO APENAS NO SAVECHANGES
    // Quando eu encontrar um jeito melhor de fazer isso, eu altero
    public EntityId<T> Salvar()
    {
        Id = Id<T>.Novo();

        return this;
    }

    public void Inativar() => Inativo = true;
}
