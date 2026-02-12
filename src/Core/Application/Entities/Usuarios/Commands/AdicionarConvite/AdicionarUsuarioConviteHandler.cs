using Microsoft.EntityFrameworkCore;
using Application.Shared.Contexts;
using Application.Shared.Messaging;
using Application.Authentication.Context;
using Application.Extensions;
using Domain.Shared.Results;
using Domain.Shared.Factories;
using Domain.Usuarios;
using Domain.Usuarios.DomainEvents;
using Domain.Usuarios.Specs;

namespace Application.Entities.Usuarios.Commands.AdicionarConvite;

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
