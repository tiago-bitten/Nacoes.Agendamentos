using Domain.Shared.Entities;
using Domain.Shared.Results;
using Domain.Shared.ValueObjects;
using Domain.Usuarios.DomainEvents;

namespace Domain.Usuarios;

public sealed class User : RemovableEntity, IAggregateRoot
{
    public const int NameMaxLength = 100;
    public const int PasswordMinLength = 4;

    private readonly List<UserMinistry> _ministries = [];

    private User() { }

    private User(string name, Email email, EAuthType authType, PhoneNumber? phoneNumber = null, string? password = null)
    {
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        AuthType = authType;
        Password = password;
    }

    public string Name { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public string? Password { get; private set; }
    public PhoneNumber? PhoneNumber { get; private set; }
    public EAuthType AuthType { get; private set; }

    public IReadOnlyList<UserMinistry> Ministries => _ministries.AsReadOnly();

    public static Result<User> Create(
        string name,
        Email email,
        EAuthType authType,
        List<Guid> ministryIds,
        PhoneNumber? phoneNumber = null,
        string? password = null)
    {
        name = name.Trim();

        if (string.IsNullOrWhiteSpace(name))
        {
            return UserErrors.NameRequired;
        }

        if (authType is not EAuthType.Local && !string.IsNullOrWhiteSpace(password))
        {
            return UserErrors.PasswordNotRequired;
        }

        if (ministryIds.Count == 0)
        {
            return UserErrors.MinistriesRequired;
        }

        var user = new User(name, email, authType, phoneNumber, password);

        foreach (var ministryId in ministryIds)
        {
            var linkResult = user.LinkMinistry(ministryId);
            if (linkResult.IsFailure)
            {
                return linkResult.Error;
            }
        }

        user.Raise(new UserAddedDomainEvent(user.Id));

        return user;
    }

    public Result LinkMinistry(Guid ministryId)
    {
        var existingLink = _ministries.SingleOrDefault(x => x.MinistryId == ministryId);
        if (existingLink is null)
        {
            _ministries.Add(new UserMinistry(ministryId));
            return Result.Success();
        }

        return existingLink.Restore();
    }

    public Result UnlinkMinistry(Guid ministryId)
    {
        var userMinistry = _ministries.SingleOrDefault(x => x.MinistryId == ministryId);
        if (userMinistry is null)
        {
            return UserErrors.MinistryNotLinkedToUser;
        }

        return userMinistry.Remove();
    }

    public Result SetPassword(string password)
    {
        if (password.Length < PasswordMinLength)
        {
            return UserErrors.PasswordTooShort;
        }

        Password = password;

        return Result.Success();
    }
}

public enum EAuthType
{
    Local = 0,
    Google = 1,
}
