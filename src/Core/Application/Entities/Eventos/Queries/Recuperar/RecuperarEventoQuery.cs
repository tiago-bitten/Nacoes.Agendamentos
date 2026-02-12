using Application.Shared.Messaging;
using Domain.Eventos;

namespace Application.Entities.Eventos.Queries.Recuperar;

public sealed record RecuperarEventoQuery(DateOnly DataInicial, DateOnly DataFinal) : IQuery<List<Evento>>;
