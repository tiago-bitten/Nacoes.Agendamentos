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

internal sealed class AdicionarUsuarioHandler(
    INacoesDbContext context)
    : ICommandHandler<AdicionarUsuarioCommand, Guid>
{
    public async Task<Result<Guid>> HandleAsync(AdicionarUsuarioCommand command, CancellationToken cancellationToken = default)
    {
        var existeUsuarioComEmail = await context.Usuarios
            .ApplySpec(new UsuarioComEmailAddressSpec(command.Email))
            .AnyAsync(cancellationToken);
        if (existeUsuarioComEmail)
        {
            return UsuarioErrors.EmailEmUso;
        }

        var usuarioResult = Usuario.Criar(
            command.Nome,
            command.Email,
            command.AuthType,
            command.MinisteriosIds,
            command.Celular is not null
                ? new Celular(command.Celular.Ddd, command.Celular.Numero)
                : null,
            command.Senha);
        if (usuarioResult.IsFailure)
        {
            return usuarioResult.Error;
        }

        var usuario = usuarioResult.Value;

        var senhaResult = usuario.DefinirSenha(PasswordHelper.Hash(command.Senha!));
        if (senhaResult.IsFailure)
        {
            return senhaResult.Error;
        }

        await context.Usuarios.AddAsync(usuario, cancellationToken);
        usuario.Raise(new UsuarioAdicionadoDomainEvent(usuario.Id));

        await context.SaveChangesAsync(cancellationToken);

        return usuario.Id;
    }
}
