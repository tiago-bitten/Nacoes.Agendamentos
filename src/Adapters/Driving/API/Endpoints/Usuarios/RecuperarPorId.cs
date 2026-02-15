using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Extensions;
using API.Infra;
using Application.Shared.Contexts;
using Domain.Shared.Results;

namespace API.Endpoints.Usuarios;

internal sealed class RecuperarPorId : IEndpoint
{
    public sealed record Response
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
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
        app.MapGet("v1/users/{id:guid}", async (
            [FromServices] INacoesDbContext context,
            [FromRoute] Guid id,
            CancellationToken ct) =>
        {
            Result<Response> result;

            try
            {
                var user = await context.Users
                    .AsNoTracking()
                    .Select(x => new Response
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Email = x.Email,
                        Ministries = x.Ministries.Select(m => new Response.MinistryItem
                        {
                            Id = m.Id,
                            Name = m.Ministry.Name,
                            Color = m.Ministry.Color.ToCssString()
                        }).ToList()
                    }).SingleOrDefaultAsync(x => x.Id == id, ct);

                result = Result<Response>.Success(user);
            }
            catch (Exception ex)
            {
                var error = Error.Problem("GetUserById", ex.Message);
                result = Result<Response>.Failure(error);
            }

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Users);
    }
}
