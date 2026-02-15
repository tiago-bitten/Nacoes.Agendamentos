using Domain.Shared.Entities;
using Domain.Ministerios;

namespace Domain.Usuarios;

public sealed class UserMinistry : RemovableEntity
{
    private UserMinistry() { }

    internal UserMinistry(Guid ministryId)
    {
        MinistryId = ministryId;
    }

    public Guid UserId { get; private set; }
    public Guid MinistryId { get; private set; }

    public User User { get; private set; } = null!;
    public Ministry Ministry { get; private set; } = null!;
}
