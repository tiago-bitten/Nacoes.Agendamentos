using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Domain.Entities.Usuarios.Interfaces;

public interface IUsuarioRepository : IBaseRepository<Usuario>
{
    IQueryable<Usuario> RecuperarPorEmailAddress(string emailAddress);
}
