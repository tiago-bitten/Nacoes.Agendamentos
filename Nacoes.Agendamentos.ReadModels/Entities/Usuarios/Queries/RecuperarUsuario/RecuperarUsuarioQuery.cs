using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Infra.Contexts;
using Nacoes.Agendamentos.ReadModels.Abstracts;
using Nacoes.Agendamentos.ReadModels.Entities.Usuarios.Queries.RecuperarUsuarios;
using Nacoes.Agendamentos.ReadModels.Extensions;

namespace Nacoes.Agendamentos.ReadModels.Entities.Usuarios.Queries.RecuperarUsuario;

public sealed class RecuperarUsuarioQuery(NacoesDbContext dbContext) : BaseQuery(dbContext), IRecuperarUsuarioQuery
{
    public async Task<RecuperarUsuarioResponse> ExecutarAsync(RecuperarUsuarioParams @params)
    {
        var query = DbContext.Usuarios.AsNoTracking();
        var response = new RecuperarUsuarioResponse
        {
            Total = await query.CountAsync()
        };

        if (!string.IsNullOrEmpty(@params.Nome))
        {
            query = query.Where(x => EF.Functions.ILike(x.Nome, $"%{@params.Nome}%"));
        }

        query = query.PaginarPorKeyset(@params.Take, @params.UltimoId, @params.UltimaDataCriacao);

        response.Items = await query.Select(x => new RecuperarUsuarioResponse.Item
                                    {
                                        Id = x.Id,
                                        Nome = x.Nome,
                                        Email = x.Email,
                                        DataCriacao = x.DataCriacao
                                    }).ToListAsync();

        response.UltimoId = response.Items.LastOrDefault()?.Id;
        response.UltimaDataCriacao = response.Items.LastOrDefault()?.DataCriacao;
        return response;
    }
}
