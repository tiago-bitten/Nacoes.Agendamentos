using FluentValidation;
using Domain.Ministerios;

namespace Application.Entities.Ministerios.Commands.AdicionarAtividade;

internal sealed class AddActivityCommandValidator : AbstractValidator<AddActivityCommand>
{
    public AddActivityCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(Activity.NameMaxLength);
    }
}
