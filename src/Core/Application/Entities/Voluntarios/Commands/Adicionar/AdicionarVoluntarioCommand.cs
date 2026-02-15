using Application.Shared.Messaging;
using Application.Common.Dtos;
using Domain.Voluntarios;
using Domain.Shared.ValueObjects;

namespace Application.Entities.Voluntarios.Commands.Adicionar;

public sealed record AddVolunteerCommand(
    string Name,
    string? Email,
    PhoneNumberItemDto? PhoneNumber,
    string? Cpf,
    DateOnly? BirthDate,
    EVolunteerRegistrationOrigin RegistrationOrigin) : ICommand<Guid>;
