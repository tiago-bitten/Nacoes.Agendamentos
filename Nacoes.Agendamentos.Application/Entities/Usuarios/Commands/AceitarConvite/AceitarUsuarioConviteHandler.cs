﻿using FluentValidation;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Authentication.Commands.Login;
using Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.Adicionar;
using Nacoes.Agendamentos.Application.Extensions;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.Interfaces;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.AceitarConvite;

public sealed class AceitarUsuarioConviteHandler(IUnitOfWork uow,
                                                 IValidator<AceitarUsuarioConviteCommand> commandValidator,
                                                 IUsuarioConviteRepository usuarioConviteRepository,
                                                 ICommandHandler<AdicionarUsuarioCommand, Guid> adicionarUsuarioHandler,
                                                 ICommandHandler<LoginCommand, LoginResponse> loginHandler)
    : ICommandHandler<AceitarUsuarioConviteCommand>
{
    public async Task<Result> Handle(AceitarUsuarioConviteCommand command, CancellationToken cancellationToken = default)
    {
        var commandResult = await commandValidator.CheckAsync(command, cancellationToken);
        if (commandResult.IsFailure)
        {
            return commandResult.Error;
        }
        
        var usuarioConvite = await usuarioConviteRepository.GetByIdAsync(command.UsuarioConviteId);
        if (usuarioConvite is null)
        {
            return UsuarioConviteErrors.ConviteNaoEncontrado;
        }

        var usuarioResult = await adicionarUsuarioHandler.Handle(
            command.ToAdicionarUsuarioCommand(usuarioConvite.Nome, usuarioConvite.Email), cancellationToken);
        if (usuarioResult.IsFailure)
        {
            return usuarioResult.Error;
        }

        await uow.BeginAsync();
        var usuario = usuarioResult.Value;
        
        var aceitarUsuarioConviteResult = usuarioConvite.Aceitar(usuario);
        if (aceitarUsuarioConviteResult.IsFailure)
        {
            return aceitarUsuarioConviteResult.Error;
        }
        
        await usuarioConviteRepository.UpdateAsync(usuarioConvite);
        await uow.CommitAsync(cancellationToken);
        
        var loginResult = await loginHandler.Handle(command.ToLoginCommand(usuarioConvite.Email), cancellationToken);
        if (loginResult.IsFailure)
        {
            return loginResult.Error;
        }
        
        return Result.Success();
    }
}