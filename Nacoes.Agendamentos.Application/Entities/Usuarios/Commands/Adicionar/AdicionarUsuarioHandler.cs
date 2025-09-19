using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Authentication.PasswordVerifiers;
using Nacoes.Agendamentos.Application.Extensions;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.DomainEvents;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.Specs;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.Adicionar;

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