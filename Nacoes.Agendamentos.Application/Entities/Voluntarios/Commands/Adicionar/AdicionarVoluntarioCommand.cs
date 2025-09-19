using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Common.Dtos;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.Adicionar;

public sealed record AdicionarVoluntarioCommand(
    string Nome,
    string? Email,
    CelularItemDto? Celular,
    string? Cpf,
    DateOnly? DataNascimento,
    EOrigemCadastroVoluntario OrigemCadastro) : ICommand<Guid>;
