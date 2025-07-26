using Nacoes.Agendamentos.Domain.Common;
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
    
    public Id<T> Id { get; private set; } = Id<T>.Novo();
    public DateTimeOffset DataCriacao { get; private set; } = DateTimeOffset.UtcNow;
    public bool Inativo { get; private set; }
    
    public List<IDomainEvent> DomainEvents => [.. _domainEvents];

    public Result Inativar()
    {
        if (Inativo)
        {
            // TODO: return EntityIdErrors.JaEstaInativo;
        }
        
        Inativo = true;
        return Result.Success();
    }
    
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
