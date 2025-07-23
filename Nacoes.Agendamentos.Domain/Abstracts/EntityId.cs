using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Domain.Abstracts;

public interface IEntity
{
    List<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
    void Raise(IDomainEvent domainEvent);
}

public abstract class EntityId<T> : IEntity
{
    private readonly List<IDomainEvent> _domainEvents = [];
    
    public Id<T> Id { get; private set; } = new(Guid.Empty.ToString());
    public DateTimeOffset DataCriacao { get; private set; } = DateTimeOffset.UtcNow;
    public bool Inativo { get; private set; }
    
    public List<IDomainEvent> DomainEvents => [.. _domainEvents];

    // USAR ISSO APENAS NO SAVECHANGES
    // Quando eu encontrar um jeito melhor de fazer isso, eu altero
    public EntityId<T> Salvar()
    {
        Id = Id<T>.Novo();

        return this;
    }

    public void Inativar() => Inativo = true;
    
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
