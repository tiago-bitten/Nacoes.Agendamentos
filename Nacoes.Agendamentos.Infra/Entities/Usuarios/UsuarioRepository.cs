using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.Interfaces;
using Nacoes.Agendamentos.Infra.Abstracts;
using Nacoes.Agendamentos.Infra.Contexts;

namespace Nacoes.Agendamentos.Infra.Entities.Usuarios;
internal class UsuarioRepository(NacoesDbContext dbContext) : BaseRepository<Usuario>(dbContext), IUsuarioRepository
{
    #region RecuperarPorEmailAddress
    public IQueryable<Usuario> RecuperarPorEmailAddress(string emailAddress)
    {
        return GetAll()
            .Where(x => x.Email.Address == emailAddress);
    }
    #endregion
}
