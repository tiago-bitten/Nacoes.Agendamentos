﻿using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Authentication.PasswordVerifiers;
using Nacoes.Agendamentos.Application.Entities.Usuarios.Mappings;
using Nacoes.Agendamentos.Application.Extensions;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.DomainEvents;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.Interfaces;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.Adicionar;

internal sealed class AdicionarUsuarioHandler(
    INacoesDbContext context, 
    IUsuarioRepository usuarioRepository)
    : ICommandHandler<AdicionarUsuarioCommand, Guid>
{
    public async Task<Result<Guid>> Handle(AdicionarUsuarioCommand command, CancellationToken cancellationToken = default)
    {
        var existeUsuarioComEmail = await usuarioRepository.RecuperarPorEmailAddress(command.Email)
                                                           .AnyAsync(cancellationToken);
        if (existeUsuarioComEmail)
        {
            return UsuarioErrors.EmailEmUso;
        }

        var usuarioResult = command.ToDomain();
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

        await usuarioRepository.AddAsync(usuario);
        
        usuario.Raise(new UsuarioAdicionadoDomainEvent(usuario.Id));
        await context.SaveChangesAsync(cancellationToken);

        return usuario.Id;
    }
}