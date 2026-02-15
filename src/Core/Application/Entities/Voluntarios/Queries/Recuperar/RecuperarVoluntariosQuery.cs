using Application.Shared.Messaging;
using Application.Shared.Pagination;

namespace Application.Entities.Voluntarios.Queries.Recuperar;

public sealed record GetVolunteersQuery : BaseQueryParam, IQuery<PagedResponse<VolunteerResponse>>
{

}
