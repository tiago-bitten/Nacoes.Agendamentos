using Domain.Shared.Entities;
using Domain.Ministerios;

namespace Domain.Voluntarios;

public sealed class VolunteerMinistry : RemovableEntity
{
    private VolunteerMinistry() { }

    internal VolunteerMinistry(Guid ministryId)
    {
        MinistryId = ministryId;
    }

    public Guid MinistryId { get; private set; }

    public Ministry Ministry { get; private set; } = null!;
    public Volunteer Volunteer { get; private set; } = null!;
}
