using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Common.Pagination;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Queries.Recuperar;

public sealed record RecuperarVoluntariosQuery : BaseQueryParam, IQuery<PagedResponse<VoluntarioResponse>>
{
    
} 