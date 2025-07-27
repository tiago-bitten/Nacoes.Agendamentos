﻿using FluentValidation;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.Desvincular;

public sealed class DesvincularVoluntarioMinisterioCommandValidator : AbstractValidator<DesvincularVoluntarioMinisterioCommand>
{
    public DesvincularVoluntarioMinisterioCommandValidator()
    {
        RuleFor(x => x.VoluntarioMinisterioId).NotEmpty().NotNull();
    }
}
