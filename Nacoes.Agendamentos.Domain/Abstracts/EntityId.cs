using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Domain.Abstracts;

public interface IEntity<T>
{
    Id<T> Id { get; }
}

public abstract class EntityId<T> : IEntity<T> where T : class
{
    public Id<T> Id { get; private set; } = new(Guid.Empty.ToString());
    public DateTimeOffset DataCriacao { get; private set; } = DateTimeOffset.UtcNow;
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
