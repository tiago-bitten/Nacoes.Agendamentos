using Domain.Shared.Entities;
using Domain.Shared.Results;
using Domain.Ministerios.DomainEvents;

namespace Domain.Ministerios;

public sealed class Activity : RemovableEntity
{
    public const int NameMaxLength = 100;
    public const int DescriptionMaxLength = 500;

    private Activity() { }

    private Activity(string name, string? description = null)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public Guid MinistryId { get; private set; }

    public Ministry Ministry { get; private set; } = null!;

    internal static Result<Activity> Create(string name, string? description = null)
    {
        name = name.Trim();
        description = description?.Trim();

        if (string.IsNullOrWhiteSpace(name))
        {
            return ActivityErrors.NameRequired;
        }

        var activity = new Activity(name, description);

        activity.Raise(new ActivityAddedDomainEvent(activity.Id));

        return activity;
    }
}
