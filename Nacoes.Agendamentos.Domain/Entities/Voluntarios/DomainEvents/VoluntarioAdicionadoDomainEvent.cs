﻿using Nacoes.Agendamentos.Domain.Abstracts;
using VoluntarioId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Voluntarios.Voluntario>;

namespace Nacoes.Agendamentos.Domain.Entities.Voluntarios.DomainEvents;

public sealed record VoluntarioAdicionadoDomainEvent(VoluntarioId VoluntarioId) : IDomainEvent;