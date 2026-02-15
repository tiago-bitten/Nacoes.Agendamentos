using Domain.Shared.Entities;
using Domain.Shared.Results;
using Domain.Ministerios;

namespace Domain.Usuarios;

public sealed class UserInvitationMinistry : RemovableEntity
{
    private UserInvitationMinistry() { }

    private UserInvitationMinistry(Guid invitationId, Guid ministryId)
    {
        InvitationId = invitationId;
        MinistryId = ministryId;
    }

    public Guid InvitationId { get; private set; }
    public Guid MinistryId { get; private set; }

    public UserInvitation Invitation { get; private set; } = null!;
    public Ministry Ministry { get; private set; } = null!;

    public static Result<UserInvitationMinistry> Create(Guid invitationId, Guid ministryId)
    {
        if (invitationId == Guid.Empty)
        {
            return UserInvitationMinistryErrors.InvitationRequired;
        }

        if (ministryId == Guid.Empty)
        {
            return UserInvitationMinistryErrors.MinistryRequired;
        }

        return new UserInvitationMinistry(invitationId, ministryId);
    }
}
