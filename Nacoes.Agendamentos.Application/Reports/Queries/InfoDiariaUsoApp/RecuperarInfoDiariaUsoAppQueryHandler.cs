using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Application.Reports.Queries.InfoDiariaUsoApp;

public sealed class RecuperarInfoDiariaUsoAppQueryHandler(INacoesDbContext context) 
    : IQueryHandler<RecuperarInfoDiariaUsoAppQuery, RecuperarInfoDiariaUsoAppResponse>
{
    public async Task<Result<RecuperarInfoDiariaUsoAppResponse>> Handle(RecuperarInfoDiariaUsoAppQuery query,
        CancellationToken cancellationToken = default)
    {
        var dataHoje = DateTimeOffset.UtcNow.AddHours(-3); // todo: criar extensao para fuso brasilia
        
        var queryOrigensCadastrosVoluntarios = context.Voluntarios
            .Where(x => x.DataCriacao.Date == dataHoje.Date)
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

        return Result<RecuperarInfoDiariaUsoAppResponse>.Success(response);
    }
}