using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Infra.Contexts;
using Nacoes.Agendamentos.ReadModels.Abstracts;
using Nacoes.Agendamentos.ReadModels.Extensions;

namespace Nacoes.Agendamentos.ReadModels.Entities.Voluntarios.Queries.RecuperarVoluntario;

public sealed class RecuperarVoluntarioQuery(NacoesDbContext dbContext) 
    : BaseQuery(dbContext), IRecuperarVoluntarioQuery
{
    public async Task<RecuperarVoluntarioResponse> ExecutarAsync(RecuperarVoluntarioParam param)
    {
        var query = DbContext.Voluntarios
            .AsNoTracking()
            .Include(x => x.Ministerios)
            .AsSplitQuery();

        var response = new RecuperarVoluntarioResponse
        {
            Total = await query.CountAsync()
        };

        if (!string.IsNullOrEmpty(param.Nome))
        {
            query = query.Where(x => x.Nome == param.Nome);
        }

        if (param.MinisteriosIds.Any())
        {
            query = query.Where(x => x.Ministerios.Any(y => param.MinisteriosIds.Contains(y.Id)));
        }

        query = query.PaginarPorKeyset(param.Take, param.UltimoId, param.UltimaDataCriacao);

        response.Items = await query
            .Select(x => new RecuperarVoluntarioResponse.Item
            {
                Id = x.Id,
                Nome = x.Nome,
                Email = x.Email != null ? x.Email.Address : string.Empty,
                DataCriacao = x.DataCriacao,
                Ministerios = (from mi in DbContext.Ministerios
                               join vm in x.Ministerios on mi.Id equals vm.MinisterioId
                               select new RecuperarVoluntarioResponse.MinisterioItem
                               {
                                   Nome = mi.Nome,
                               }).ToList()
            })
            .ToListAsync();

        var lastItem = response.Items.LastOrDefault();
        response.UltimoId = lastItem?.Id;
        response.UltimaDataCriacao = lastItem?.DataCriacao;

        return response;
    }
}