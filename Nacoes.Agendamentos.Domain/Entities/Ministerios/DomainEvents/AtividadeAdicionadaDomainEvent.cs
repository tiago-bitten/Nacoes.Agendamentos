using Nacoes.Agendamentos.Domain.Abstracts;
using AtividadeId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Ministerios.Atividade>;

namespace Nacoes.Agendamentos.Domain.Entities.Ministerios.DomainEvents;

public sealed record AtividadeAdicionadaDomainEvent(AtividadeId AtividadeId) : IDomainEvent;
