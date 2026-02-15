using FluentValidation;
using Domain.Ministerios;

namespace Application.Entities.Ministerios.Commands.Adicionar;

internal sealed class AddMinistryCommandValidator : AbstractValidator<AddMinistryCommand>
{
    public AddMinistryCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(Ministry.NameMaxLength);
    }
}
