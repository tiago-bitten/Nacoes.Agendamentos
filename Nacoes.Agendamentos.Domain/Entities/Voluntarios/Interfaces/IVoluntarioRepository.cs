using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;

namespace Nacoes.Agendamentos.Domain.Entities.Voluntarios.Interfaces;

public interface IVoluntarioRepository : IBaseRepository<Voluntario>
{
    IQueryable<Voluntario> RecuperarPorVoluntarioMinisterio(Guid voluntarioMinisterioId);
    IQueryable<Voluntario> RecuperarPorEmailAddress(string emailAddress);
    IQueryable<Voluntario> RecuperarParaLoginExterno(DateOnly dataNascimento, string cpf);
}
