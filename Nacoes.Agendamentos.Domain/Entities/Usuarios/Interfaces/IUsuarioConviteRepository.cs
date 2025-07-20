using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;

namespace Nacoes.Agendamentos.Domain.Entities.Usuarios.Interfaces;

public interface IUsuarioConviteRepository : IBaseRepository<UsuarioConvite>
{
    IQueryable<UsuarioConvite> RecuperarAguardandoAceite();
}