using Nacoes.Agendamentos.Application.Entities.Voluntarios.Dtos;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Interfaces;

public interface IVoluntarioApplicationRepository
{
    Task<LoginVoluntarioDto?> RecuperarLoginAsync(string cpf, DateOnly dataNascimento);
}

