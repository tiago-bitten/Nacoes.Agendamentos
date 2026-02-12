using Domain.Shared.Events;

namespace Domain.Shared.Entities;

public interface IEntity
{
    Guid Id { get; }
    List<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
    void Raise(IDomainEvent domainEvent);
}
