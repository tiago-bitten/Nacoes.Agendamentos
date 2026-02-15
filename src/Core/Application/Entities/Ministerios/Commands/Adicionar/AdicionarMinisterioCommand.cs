using Application.Shared.Messaging;
using Application.Common.Dtos;

namespace Application.Entities.Ministerios.Commands.Adicionar;

public sealed record AddMinistryCommand(string Name, string? Description, ColorDto Color) : ICommand<Guid>;
