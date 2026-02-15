using Domain.Shared.Entities;
using Domain.Shared.Results;
using Domain.Shared.ValueObjects;
using Domain.Ministerios.DomainEvents;

namespace Domain.Ministerios;

public sealed class Ministry : RemovableEntity, IAggregateRoot
{
    public const int NameMaxLength = 100;
    public const int DescriptionMaxLength = 500;

    private readonly List<Activity> _activities = [];

    private Ministry() { }

    private Ministry(string name, string? description, Color color)
    {
        Name = name;
        Description = description;
        Color = color;
    }

    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }
    public Color Color { get; private set; } = null!;

    public IReadOnlyCollection<Activity> Activities => _activities.AsReadOnly();

    public void UpdateName(string name) => Name = name;

    public void UpdateDescription(string description) => Description = description;

    public void UpdateColor(Color color) => Color = color;

    public static Result<Ministry> Create(string name, string? description, Color color)
    {
        name = name.Trim();
        description = description?.Trim();

        if (string.IsNullOrWhiteSpace(name))
        {
            return MinistryErrors.NameRequired;
        }

        var ministry = new Ministry(name, description, color);

        ministry.Raise(new MinistryAddedDomainEvent(ministry.Id));

        return ministry;
    }

    public Result<Activity> AddActivity(string name, string? description)
    {
        var activityResult = Activity.Create(name, description);
        if (activityResult.IsFailure)
        {
            return activityResult.Error;
        }

        var activity = activityResult.Value;
        _activities.Add(activity);

        return Result<Activity>.Success(activity);
    }
}
