using Nacoes.Agendamentos.Domain.Abstracts;

namespace Nacoes.Agendamentos.Domain.Entities.Eventos.DomainEvents;

public sealed record EventoAdicionadoDomainEvent(Guid EventoId) : IDomainEvent;
