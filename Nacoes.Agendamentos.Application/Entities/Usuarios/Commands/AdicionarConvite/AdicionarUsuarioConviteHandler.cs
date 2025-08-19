using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Application.Abstracts.Data;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Authentication.Context;
using Nacoes.Agendamentos.Application.Extensions;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Common.Factories;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.DomainEvents;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.Specs;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.AdicionarConvite;

internal sealed class AdicionarUsuarioConviteHandler(
    INacoesDbContext context, 
    IUsuarioConviteRepository usuarioConviteRepository, 
    IAmbienteContext ambienteContext, 
    ILinkFactory linkFactory)               
    : ICommandHandler<AdicionarUsuarioConviteCommand, UsuarioConviteResponse>
{
    public async Task<Result<UsuarioConviteResponse>> Handle(AdicionarUsuarioConviteCommand command, CancellationToken cancellationToken = default)
    {
        var statusAguardandoAceite = await context.Convites
            .WhereSpec(new ConvitesPendentesSpec())
            .Where(x => x.Email.Address == command.Email)
            .Select(x => x.Status)
            .FirstOrDefaultAsync(cancellationToken);
            
        if (statusAguardandoAceite is EConviteStatus.Pendente)
        {
            return UsuarioConviteErrors.ConvitePendente;
        }

        var usuarioConviteResult = UsuarioConvite.Criar(command.Nome, command.Email, ambienteContext.UserId);
        if (usuarioConviteResult.IsFailure)
        {
            return usuarioConviteResult.Error;
        }
        
        var usuarioConvite = usuarioConviteResult.Value;
        
        var link = linkFactory.Create(usuarioConvite.Path);
        var response = new UsuarioConviteResponse
        {
            Link = link
        };
        
        usuarioConvite.Raise(new UsuarioConviteAdicionadoDomainEvent(usuarioConvite.Id, link));
        
        await context.SaveChangesAsync(cancellationToken);
        
        return Result<UsuarioConviteResponse>.Success(response);
    }
}