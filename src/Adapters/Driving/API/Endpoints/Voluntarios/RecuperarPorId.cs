using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Extensions;
using API.Infra;
using Application.Shared.Contexts;
using Domain.Shared.Results;
using Domain.Voluntarios;

namespace API.Endpoints.Voluntarios;

internal sealed class RecuperarPorId : IEndpoint
{
    public sealed record Response
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string? Email { get; init; }
        public string? PhoneNumber { get; init; }
        public string? Cpf { get; init; }
        public DateOnly? BirthDate { get; init; }
        public EVolunteerRegistrationOrigin RegistrationOrigin { get; init; }
        public List<MinistryItem> Ministries { get; init; } = [];

        public sealed record MinistryItem
        {
            public Guid Id { get; init; }
            public string Name { get; init; } = string.Empty;
            public string Color { get; init; } = string.Empty;
        }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("v1/volunteers/{id:guid}", async (
            [FromServices] INacoesDbContext context,
            [FromRoute] Guid id,
            CancellationToken ct) =>
        {
            Result<Response> result;

            try
            {
                var volunteer = await context.Volunteers
                    .AsNoTracking()
                    .Select(x => new Response
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Email = x.EmailAddress,
                        PhoneNumber = x.PhoneNumber !.ToString(),
                        Cpf = x.Cpf != null ? x.Cpf.Number : null,
                        BirthDate = x.BirthDate != null ? x.BirthDate.Value : null,
                        RegistrationOrigin = x.RegistrationOrigin,
                        Ministries = x.Ministries.Select(m => new Response.MinistryItem
                        {
                            Id = m.Id,
                            Name = m.Ministry.Name,
                            Color = m.Ministry.Color.ToCssString()
                        }).ToList()
                    }).SingleOrDefaultAsync(x => x.Id == id, ct);

                result = Result<Response>.Success(volunteer);
            }
            catch (Exception ex)
            {
                var error = Error.Problem("GetVolunteerById", ex.Message);
                result = Result<Response>.Failure(error);
            }

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Volunteers);
    }
}
