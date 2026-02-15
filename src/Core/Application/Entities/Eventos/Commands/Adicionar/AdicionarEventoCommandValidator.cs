using FluentValidation;
using Domain.Eventos;
using Domain.Shared.ValueObjects;

namespace Application.Entities.Eventos.Commands.Adicionar;

internal sealed class AddEventCommandValidator : AbstractValidator<AddEventCommand>
{
    public AddEventCommandValidator()
    {
        RuleFor(x => x.Schedule)
            .NotNull();

        RuleFor(x => x.Schedule.StartDate)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Schedule.EndDate)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Description)
            .NotEmpty()
            .NotNull()
            .MaximumLength(Event.DescriptionMaxLength);

        RuleFor(x => x.Schedule.StartDate)
            .LessThan(x => x.Schedule.EndDate);

        RuleFor(x => x.Recurrence.Type)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Recurrence.Interval)
            .NotEmpty()
            .NotNull()
            .When(x => x.Recurrence.Type is not EEventRecurrenceType.None);

        RuleFor(x => x.Recurrence.EndDate)
            .NotEmpty()
            .NotNull()
            .When(x => x.Recurrence.Type is not EEventRecurrenceType.None);

        RuleFor(x => x.MaxReservationCount)
            .GreaterThan(0)
            .When(x => x.MaxReservationCount.HasValue);
    }
}
