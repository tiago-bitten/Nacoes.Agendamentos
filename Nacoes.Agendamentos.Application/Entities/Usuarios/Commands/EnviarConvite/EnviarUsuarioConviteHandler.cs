using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Authentication.Context;
using Nacoes.Agendamentos.Application.Extensions;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.Interfaces;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.EnviarConvite;

public sealed class EnviarUsuarioConviteHandler(IUnitOfWork uow,
                                                IValidator<EnviarUsuarioConviteCommand> commandValidator,
                                                IUsuarioConviteRepository usuarioConviteRepository,
                                                IAmbienteContext ambienteContext)               
    : ICommandHandler<EnviarUsuarioConviteCommand, string>
{
    public async Task<Result<string>> Handle(EnviarUsuarioConviteCommand command, CancellationToken cancellationToken = default)
    {
        var commandResult = await commandValidator.CheckAsync(command, cancellationToken);
        if (commandResult.IsFailure)
        {
            return commandResult.Error;
        }

        var existeAguardandoAceite = await usuarioConviteRepository.RecuperarAguardandoAceite()
                                                                   .Where(x => x.Email == command.Email)
                                                                   .SingleOrDefaultAsync(cancellationToken);
        if (existeAguardandoAceite?.Status is EConviteStatus.Pendente)
        {
            return UsuarioConviteErrors.ConvitePendente;
        }

        await uow.BeginAsync();
        if (existeAguardandoAceite?.Status is EConviteStatus.Enviado)
        {
            var usuarioConviteCanceladoResult = existeAguardandoAceite.Cancelar("Um novo convite foi enviado.");
            if (usuarioConviteCanceladoResult.IsFailure)
            {
                await uow.RollbackAsync();
                return usuarioConviteCanceladoResult.Error;
            }
            await usuarioConviteRepository.UpdateAsync(existeAguardandoAceite);
        }

        var usuarioConviteResult = UsuarioConvite.Criar(command.Nome, command.Email, ambienteContext.UsuarioId);
        if (usuarioConviteResult.IsFailure)
        {
            return usuarioConviteResult.Error;
        }
        
        var usuarioConvite = usuarioConviteResult.Value;
        await usuarioConviteRepository.AddAsync(usuarioConvite);
        await uow.CommitAsync(cancellationToken);
        // TODO: Histórico service: Convite enviado
        
        // TODO: Enviar email
        
        usuarioConvite.Pendenciar();
        await usuarioConviteRepository.UpdateAsync(usuarioConvite);
        await uow.CommitAsync(cancellationToken);
        // TODO: Histórico service: Convite pendente
        
        
        return Result<string>.Success(usuarioConvite.Token);
    }
}