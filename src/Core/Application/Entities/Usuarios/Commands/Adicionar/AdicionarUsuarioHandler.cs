using Microsoft.EntityFrameworkCore;
using Application.Shared.Contexts;
using Application.Shared.Messaging;
using Application.Authentication.PasswordVerifiers;
using Application.Extensions;
using Domain.Shared.Results;
using Domain.Usuarios;
using Domain.Usuarios.DomainEvents;
using Domain.Usuarios.Specs;
using Domain.Shared.ValueObjects;

namespace Application.Entities.Usuarios.Commands.Adicionar;

internal sealed class AddUserHandler(
    INacoesDbContext context)
    : ICommandHandler<AddUserCommand, Guid>
{
    public async Task<Result<Guid>> HandleAsync(
        AddUserCommand command,
        CancellationToken ct)
    {
        var userWithEmailExists = await context.Users
            .ApplySpec(new UserWithEmailAddressSpec(command.Email))
            .AnyAsync(ct);
        if (userWithEmailExists)
        {
            return UserErrors.EmailInUse;
        }

        var userResult = User.Create(
            command.Name,
            command.Email,
            command.AuthType,
            command.MinistryIds,
            command.PhoneNumber is not null
                ? new PhoneNumber(command.PhoneNumber.AreaCode, command.PhoneNumber.Number)
                : null,
            command.Password);
        if (userResult.IsFailure)
        {
            return userResult.Error;
        }

        var user = userResult.Value;

        var passwordResult = user.SetPassword(PasswordHelper.Hash(command.Password!));
        if (passwordResult.IsFailure)
        {
            return passwordResult.Error;
        }

        await context.Users.AddAsync(user, ct);
        user.Raise(new UserAddedDomainEvent(user.Id));

        await context.SaveChangesAsync(ct);

        return user.Id;
    }
}
