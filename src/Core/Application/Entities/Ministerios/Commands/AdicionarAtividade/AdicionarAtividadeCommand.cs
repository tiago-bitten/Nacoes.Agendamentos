using Application.Shared.Messaging;

namespace Application.Entities.Ministerios.Commands.AdicionarAtividade;

public sealed record AddActivityCommand(string Name, string? Description, Guid MinistryId) : ICommand<Guid>;
