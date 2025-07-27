﻿using Nacoes.Agendamentos.Application.Abstracts.Messaging;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.Desvincular;

public sealed record DesvincularVoluntarioMinisterioCommand : ICommand
{
    public Guid VoluntarioMinisterioId { get; init; }
}
