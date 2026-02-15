using Domain.Shared.Entities;
using Domain.Shared.Results;
using Domain.Usuarios.DomainEvents;
using Domain.Shared.ValueObjects;

namespace Domain.Usuarios;

public sealed class UserInvitation : RemovableEntity, IAggregateRoot
{
    public const int NameMaxLength = 100;
    public const int ReasonMaxLength = 500;
    public const int TokenMaxLength = 64;

    private const int ExpirationInDays = 7;
    private readonly List<UserInvitationMinistry> _ministries = [];

    private UserInvitation() { }

    private UserInvitation(
        string name, Email email,
        Guid sentById,
        EInvitationStatus status,
        DateTimeOffset expirationDate,
        string token)
    {
        Name = name;
        Email = email;
        SentById = sentById;
        Status = status;
        ExpirationDate = expirationDate;
        Token = token;
    }

    public string Name { get; private set; } = string.Empty;
    public Email Email { get; private set; } = null!;
    public Guid SentById { get; private set; }
    public Guid? SentToId { get; private set; }
    public EInvitationStatus Status { get; private set; }
    public string Reason { get; private set; } = string.Empty;
    public DateTimeOffset ExpirationDate { get; private set; }
    public string Token { get; private set; } = string.Empty;

    public User SentBy { get; private set; } = null!;
    public User? SentTo { get; private set; }
    public IReadOnlyList<UserInvitationMinistry> Ministries => _ministries.AsReadOnly();

    public string Path => $"users/invitations/{Token}";

    public static Result<UserInvitation> Create(
        string name,
        Email email,
        Guid sentById,
        List<Guid> ministryIds)
    {
        name = name.Trim();

        if (string.IsNullOrWhiteSpace(name))
        {
            return UserErrors.NameRequired;
        }

        if (ministryIds.Count == 0)
        {
            return UserErrors.MinistriesRequired;
        }

        var expirationDate = DateTimeOffset.UtcNow.AddDays(ExpirationInDays);
        var token = Guid.NewGuid().ToString("N");
        var invitation = new UserInvitation(
            name,
            email,
            sentById,
            EInvitationStatus.Pending,
            expirationDate,
            token);

        foreach (var ministryId in ministryIds)
        {
            var invitationMinistryResult = UserInvitationMinistry.Create(invitation.Id, ministryId);
            if (invitationMinistryResult.IsFailure)
            {
                return invitationMinistryResult.Error;
            }

            var invitationMinistry = invitationMinistryResult.Value;
            invitation._ministries.Add(invitationMinistry);
        }

        invitation.Raise(new UserInvitationAddedDomainEvent(invitation.Id, token));

        return invitation;
    }

    public Result Accept(Guid sentToId)
    {
        if (Status is not EInvitationStatus.Pending)
        {
            return UserInvitationErrors.InvalidStatusToAccept;
        }

        Status = EInvitationStatus.Accepted;
        SentToId = sentToId;

        return Result.Success();
    }

    public Result Decline()
    {
        if (Status is not EInvitationStatus.Pending)
        {
            return UserInvitationErrors.InvalidStatusToDecline;
        }

        Status = EInvitationStatus.Declined;

        return Result.Success();
    }

    public Result Expire()
    {
        if (Status is not EInvitationStatus.Pending)
        {
            return UserInvitationErrors.InvalidStatusToExpire;
        }

        var today = DateTimeOffset.UtcNow;
        if (ExpirationDate > today)
        {
            return UserInvitationErrors.ExpirationDateNotReached;
        }

        Status = EInvitationStatus.Expired;

        return Result.Success();
    }

    public Result Cancel(string reason)
    {
        if (Status is not EInvitationStatus.Pending)
        {
            return UserInvitationErrors.InvalidStatusToCancel;
        }

        if (string.IsNullOrWhiteSpace(reason))
        {
            return UserInvitationErrors.ReasonRequired;
        }

        Status = EInvitationStatus.Cancelled;
        Reason = reason;

        return Result.Success();
    }

    public Result Error(string? reason)
    {
        Status = EInvitationStatus.Error;
        Reason = reason ?? "An internal error occurred.";

        return Result.Success();
    }
}

public enum EInvitationStatus
{
    Pending = 0,
    Accepted = 1,
    Declined = 2,
    Expired = 3,
    Cancelled = 4,
    Error = 5
}
