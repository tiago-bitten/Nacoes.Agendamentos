using Nacoes.Agendamentos.Domain.Abstracts;
using AgendaId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Agendas.Agenda>;

namespace Nacoes.Agendamentos.Domain.Entities.Agendas;

public sealed record AgendaAdicionadaDomainEvent(AgendaId AgendaId) : IDomainEvent;

