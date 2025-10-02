using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Domain.Entities.Eventos;

namespace Nacoes.Agendamentos.Application.Entities.Eventos.Queries.Recuperar;

public sealed record RecuperarEventoQuery(DateOnly DataInicial, DateOnly DataFinal) : IQuery<List<Evento>>;
