using Domain.Shared.Entities;
using Domain.Shared.Results;
using Domain.Voluntarios.DomainEvents;
using Domain.Voluntarios.Errors;
using Domain.Shared.ValueObjects;

namespace Domain.Voluntarios;

public sealed class Volunteer : RemovableEntity, IAggregateRoot
{
    public const int NameMaxLength = 100;

    private readonly List<VolunteerMinistry> _ministries = [];

    private Volunteer() { }

    private Volunteer(
        string name,
        EVolunteerRegistrationOrigin registrationOrigin,
        Email? email,
        PhoneNumber? phoneNumber,
        CPF? cpf,
        BirthDate? birthDate)
    {
        Name = name;
        RegistrationOrigin = registrationOrigin;
        Email = email;
        PhoneNumber = phoneNumber;
        Cpf = cpf;
        BirthDate = birthDate;
    }

    public string Name { get; private set; } = null!;
    public Email? Email { get; private set; }
    public PhoneNumber? PhoneNumber { get; private set; }
    public CPF? Cpf { get; private set; }
    public BirthDate? BirthDate { get; private set; }
    public EVolunteerRegistrationOrigin RegistrationOrigin { get; private set; }

    public IReadOnlyCollection<VolunteerMinistry> Ministries => _ministries.AsReadOnly();

    public string EmailAddress => Email?.Address ?? string.Empty;
    public int Age => BirthDate?.Age ?? 0;
    public bool IsMinor => BirthDate?.IsMinor ?? false;

    public static Result<Volunteer> Create(
        string name,
        Email? email,
        PhoneNumber? phoneNumber,
        CPF? cpf,
        BirthDate? birthDate,
        EVolunteerRegistrationOrigin registrationOrigin)
    {
        name = name.Trim();

        if (string.IsNullOrWhiteSpace(name))
        {
            return VolunteerErrors.NameRequired;
        }

        if (registrationOrigin is not EVolunteerRegistrationOrigin.System
            && (email is null || cpf is null || birthDate is null))
        {
            return VolunteerErrors.PersonalDataRequired;
        }

        var volunteer = new Volunteer(name, registrationOrigin, email, phoneNumber, cpf, birthDate);

        volunteer.Raise(new VolunteerAddedDomainEvent(volunteer.Id));

        return volunteer;
    }

    public Result LinkMinistry(Guid ministryId)
    {
        var existingLink = _ministries.SingleOrDefault(x => x.MinistryId == ministryId);
        if (existingLink is null)
        {
            _ministries.Add(new VolunteerMinistry(ministryId));
            return Result.Success();
        }

        return existingLink.Restore();
    }

    public Result UnlinkMinistry(Guid ministryId)
    {
        var existingLink = _ministries.SingleOrDefault(x => x.MinistryId == ministryId);
        if (existingLink is null)
        {
            return VolunteerMinistryErrors.VolunteerNotLinkedToMinistry;
        }

        return existingLink.Remove();
    }
}

public enum EVolunteerRegistrationOrigin
{
    System = 0,
    Website = 1,
    App = 2
}
