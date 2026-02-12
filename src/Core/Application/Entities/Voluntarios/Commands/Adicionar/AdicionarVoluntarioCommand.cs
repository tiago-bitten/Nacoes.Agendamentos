using Application.Shared.Messaging;
using Application.Common.Dtos;
using Domain.Voluntarios;
using Domain.Shared.ValueObjects;

namespace Application.Entities.Voluntarios.Commands.Adicionar;

public sealed record AdicionarVoluntarioCommand(
    string Nome,
    string? Email,
    CelularItemDto? Celular,
    string? Cpf,
    DateOnly? DataNascimento,
    EOrigemCadastroVoluntario OrigemCadastro) : ICommand<Guid>;
