using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.Interfaces;
using Nacoes.Agendamentos.Infra.Abstracts;
using Nacoes.Agendamentos.Infra.Contexts;

namespace Nacoes.Agendamentos.Infra.Entities.Usuarios;

internal sealed class UsuarioConviteRepository(NacoesDbContext dbContext) 
    : BaseRepository<UsuarioConvite>(dbContext), IUsuarioConviteRepository
{
    #region RecuperarPendentes
    public IQueryable<UsuarioConvite> RecuperarPendentes()
        => GetAll().Where(x => x.Status == EConviteStatus.Pendente);

    #endregion
    
    #region RecuperarPorToken
    public IQueryable<UsuarioConvite> RecuperarPorToken(string token)
        => GetAll().Where(x => x.Token == token);
    #endregion
}