using Microsoft.EntityFrameworkCore;
using Application.Shared.Contexts;
using Application.Shared.Ports.Notifications;
using Domain.Shared.Events;
using Domain.Historicos.Interfaces;
using Domain.Usuarios.DomainEvents;

namespace Application.Entities.Usuarios.Commands.AceitarConvite;

internal sealed class UserInvitationAcceptedDomainEventHandler(
    INacoesDbContext context,
    IAuditLogRegister auditLogRegister,
    IEmailSenderFactory emailSenderFactory,
    ITemplateRenderer templateRenderer)
    : IDomainEventHandler<UserInvitationAcceptedDomainEvent>
{
    private IEmailSender EmailSender => emailSenderFactory.Create();

    public async Task Handle(UserInvitationAcceptedDomainEvent domainEvent, CancellationToken ct)
    {
        var invitation = await context.Invitations
            .Where(x => x.Id == domainEvent.UserInvitationId)
            .Select(x => new
            {
                x.Name,
                SentByEmail = x.SentBy.Email
            }).SingleOrDefaultAsync(ct);
        if (invitation is null)
        {
            return;
        }

        await auditLogRegister.AuditAsync(domainEvent.UserInvitationId, action: "Invitation accepted.");
    }

    private (string Title, string Html) GetTemplate(string name)
    {
        var title = $"Invitation accepted by {name} - Igreja Nacoes";
        var html = templateRenderer.Render("ConviteAceito", new Dictionary<string, string>
        {
            { "NOME", name }
        });

        return (title, html);
    }
}
