﻿using FluentValidation;
using Nacoes.Agendamentos.Application.Authentication.Commands.Login;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;

namespace Nacoes.Agendamentos.Application.Authentication.Validators;

public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.AuthType).NotNull();
        RuleFor(x => x.Senha).NotEmpty().When(x => x.AuthType == EAuthType.Local);
        RuleFor(x => x.TokenExterno).NotEmpty().When(x => x.AuthType != EAuthType.Local);
    }
}
