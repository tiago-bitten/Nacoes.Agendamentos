using Microsoft.EntityFrameworkCore;
using Application.Shared.Contexts;
using Application.Shared.Ports.Notifications;
using Domain.Shared.Events;
using Domain.Historicos.Interfaces;
using Domain.Usuarios;
using Domain.Usuarios.DomainEvents;

namespace Application.Entities.Usuarios.Commands.AdicionarConvite;

internal sealed class UserInvitationAddedDomainEventHandler(INacoesDbContext context,
                                                             IEmailSenderFactory emailSenderFactory,
                                                             ITemplateRenderer templateRenderer,
                                                             IAuditLogRegister auditLogRegister)
    : IDomainEventHandler<UserInvitationAddedDomainEvent>
{
    private IEmailSender EmailSender => emailSenderFactory.Create();

    public async Task Handle(UserInvitationAddedDomainEvent domainEvent, CancellationToken ct)
    {
        await auditLogRegister.AuditAsync(domainEvent.UserInvitationId, action: "Invitation generated.");
        var invitation = await context.Invitations
            .AsNoTracking()
            .Include(x => x.SentBy)
            .SingleOrDefaultAsync(x => x.Id == domainEvent.UserInvitationId, ct);
        try
        {
            if (invitation is null)
            {
                return;
            }
            await auditLogRegister.AuditAsync(invitation, action: "Invitation sent by email.");
            var (email, title, html) = GetTemplate(invitation);
            await EmailSender.SendAsync(email, title, html);

            await auditLogRegister.AuditAsync(invitation, action: "Invitation delivered to inbox.");
        }
        catch (Exception ex)
        {
            await auditLogRegister.AuditAsync(
                invitation!,
                action: "An error occurred while sending the invitation by email.",
                details: ex.Message);
        }
    }

    private (string Email, string Title, string Html) GetTemplate(UserInvitation invitation)
    {
        var title = "Invitation - Igreja Nacoes";
        var html = templateRenderer.Render("ConviteUsuario", new Dictionary<string, string>()
        {
            { "NOME_CONVIDADO", invitation.Name },
            { "NOME_USUARIO", invitation.SentBy.Name },
            { "LINK", "http://localhost:4200/convites" }
        });

        return (invitation.Email.Address, title, html);
    }
}
