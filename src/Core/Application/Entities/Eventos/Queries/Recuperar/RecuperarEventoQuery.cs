using Application.Shared.Messaging;
using Domain.Eventos;

namespace Application.Entities.Eventos.Queries.Recuperar;

public sealed record GetEventQuery(DateOnly StartDate, DateOnly EndDate) : IQuery<List<Event>>;
