using Domain.Shared.Events;
using Domain.Shared.Results;

namespace Domain.Shared.Entities;

public abstract class Entity : IEntity
{
    private readonly List<IDomainEvent> _domainEvents = [];

    public Guid Id { get; private set; } = Guid.NewGuid();
    public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;

    public List<IDomainEvent> DomainEvents => [.. _domainEvents];

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}

public abstract class RemovableEntity : Entity
{
    public bool IsRemoved { get; private set; }

    public Result Remove()
    {
        if (IsRemoved)
        {
            return Result.Failure(
                Error.Problem("entity.already_removed", "The entity has already been removed."));
        }

        IsRemoved = true;
        return Result.Success();
    }

    public Result Restore()
    {
        if (!IsRemoved)
        {
            return Result.Failure(
                Error.Problem("entity.not_removed", "The entity is not removed."));
        }

        IsRemoved = false;
        return Result.Success();
    }
}
