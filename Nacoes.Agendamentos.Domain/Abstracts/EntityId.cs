using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Domain.Abstracts;

public interface IEntity
{
    Guid Id { get; }
    List<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
    void Raise(IDomainEvent domainEvent);
}

public abstract class EntityId : IEntity
{
    private readonly List<IDomainEvent> _domainEvents = [];

    public Guid Id { get; private set; } = Guid.NewGuid();
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

    public Result Restaurar()
    {
        if (!Inativo)
        {
            // TODO: return EntityIdErrors.JaEstaAtivo;
        }
        
        Inativo = false;
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
