using Nacoes.Agendamentos.Domain.Abstracts;
using MinisterioId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Ministerios.Ministerio>;

namespace Nacoes.Agendamentos.Domain.Entities.Ministerios.DomainEvents;

public sealed record MinisterioAdicionadoDomainEvent(MinisterioId MinisterioId) : IDomainEvent;
