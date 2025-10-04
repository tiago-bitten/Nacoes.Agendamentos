using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Authentication.Context;
using Nacoes.Agendamentos.Application.Extensions;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Common.Factories;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.DomainEvents;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.Specs;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.AdicionarConvite;

internal sealed class AdicionarUsuarioConviteHandler(
    INacoesDbContext context, 
    IAmbienteContext ambienteContext, 
    ILinkFactory linkFactory)               
    : ICommandHandler<AdicionarUsuarioConviteCommand, UsuarioConviteResponse>
{
    public async Task<Result<UsuarioConviteResponse>> HandleAsync(AdicionarUsuarioConviteCommand command, CancellationToken cancellationToken = default)
    {
        var existeConvitePendente = await context.Convites
            .ApplySpec(new ConvitesPendentesSpec())
            .Where(x => x.Email.Address == command.EmailAddress)
            .AnyAsync(cancellationToken);
            
        if (existeConvitePendente)
        {
            return UsuarioConviteErrors.ConvitePendente;
        }

        var usuarioConviteResult = UsuarioConvite.Criar(
            command.Nome,
            command.EmailAddress,
            ambienteContext.UserId,
            command.MinisteriosIds);
        if (usuarioConviteResult.IsFailure)
        {
            return usuarioConviteResult.Error;
        }
        
        var usuarioConvite = usuarioConviteResult.Value;
        
        var link = linkFactory.Create(usuarioConvite.Path);
        var response = new UsuarioConviteResponse(link);
        
        await context.Convites.AddAsync(usuarioConvite, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return response;
    }
}