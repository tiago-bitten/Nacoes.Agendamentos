using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.Interfaces;
using Nacoes.Agendamentos.Infra.Abstracts;
using Nacoes.Agendamentos.Infra.Contexts;

namespace Nacoes.Agendamentos.Infra.Entities.Usuarios;

internal sealed class UsuarioConviteRepository(NacoesDbContext dbContext) 
    : BaseRepository<UsuarioConvite>(dbContext), IUsuarioConviteRepository
{
    #region RecuperarAguardandoAceite
    public IQueryable<UsuarioConvite> RecuperarAguardandoAceite()
        => GetAll().Where(x => x.Status == EConviteStatus.Pendente || x.Status == EConviteStatus.Enviado);

    #endregion
}