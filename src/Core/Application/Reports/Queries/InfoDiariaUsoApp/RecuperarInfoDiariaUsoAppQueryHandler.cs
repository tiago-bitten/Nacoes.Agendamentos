using Microsoft.EntityFrameworkCore;
using Application.Shared.Contexts;
using Application.Shared.Messaging;
using Domain.Shared.Results;

namespace Application.Reports.Queries.InfoDiariaUsoApp;

public sealed class RecuperarInfoDiariaUsoAppQueryHandler(INacoesDbContext context)
    : IQueryHandler<RecuperarInfoDiariaUsoAppQuery, RecuperarInfoDiariaUsoAppResponse>
{
    public async Task<Result<RecuperarInfoDiariaUsoAppResponse>> Handle(RecuperarInfoDiariaUsoAppQuery query,
        CancellationToken cancellationToken = default)
    {
        var dataHoje = DateTimeOffset.UtcNow.AddHours(-3); // todo: criar extensao para fuso brasilia

        var queryOrigensCadastrosVoluntarios = context.Voluntarios
            .Where(x => x.CreatedAt.Date == dataHoje.Date)
            .GroupBy(x => x.OrigemCadastro)
            .Select(x => new RecuperarInfoDiariaUsoAppResponse.VoluntarioInfo.VoluntarioInfoOrigem
            {
                Origem = x.Key,
                Quantidade = x.Count()
            });

        var response = new RecuperarInfoDiariaUsoAppResponse
        {
            Data = dataHoje,
            Voluntarios = new RecuperarInfoDiariaUsoAppResponse.VoluntarioInfo
            {
                Origens = await queryOrigensCadastrosVoluntarios.ToListAsync(cancellationToken),
                QuantidadeTotal = await queryOrigensCadastrosVoluntarios.SumAsync(x => x.Quantidade, cancellationToken)
            }
        };

        return response;
    }
}
