﻿using FluentValidation;
using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Authentication.Commands.Login;
using Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.Adicionar;
using Nacoes.Agendamentos.Application.Extensions;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.DomainEvents;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.Interfaces;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.AceitarConvite;

internal sealed class AceitarUsuarioConviteHandler(
    INacoesDbContext context, 
    IUsuarioConviteRepository usuarioConviteRepository, 
    ICommandHandler<AdicionarUsuarioCommand, Guid> adicionarUsuarioHandler, 
    ICommandHandler<LoginCommand, LoginResponse> loginHandler)
    : ICommandHandler<AceitarUsuarioConviteCommand, AceitarUsuarioConviteResponse>
{
    public async Task<Result<AceitarUsuarioConviteResponse>> Handle(AceitarUsuarioConviteCommand command, CancellationToken cancellationToken = default)
    {
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

        var usuario = usuarioResult.Value;
        
        var aceitarUsuarioConviteResult = usuarioConvite.Aceitar(usuario);
        if (aceitarUsuarioConviteResult.IsFailure)
        {
            return aceitarUsuarioConviteResult.Error;
        }
        
        await usuarioConviteRepository.UpdateAsync(usuarioConvite);
        
        usuarioConvite.Raise(new UsuarioConviteAceitoDomainEvent(usuarioConvite.Id));
        await context.SaveChangesAsync(cancellationToken);
        
        var loginResult = await loginHandler.Handle(command.ToLoginCommand(usuarioConvite.Email), cancellationToken);
        if (loginResult.IsFailure)
        {
            return loginResult.Error;
        }
        
        var response = new AceitarUsuarioConviteResponse
        {
            AuthToken = loginResult.Value.AuthToken,
            RefreshToken = loginResult.Value.RefreshToken
        };
        
        return Result<AceitarUsuarioConviteResponse>.Success(response);
    }
}