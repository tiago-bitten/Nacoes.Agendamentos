using Nacoes.Agendamentos.Application.Abstracts.Messaging;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Queries.Recuperar;

public record RecuperarVoluntariosQuery() : IQuery<List<VoluntarioResponse>>;