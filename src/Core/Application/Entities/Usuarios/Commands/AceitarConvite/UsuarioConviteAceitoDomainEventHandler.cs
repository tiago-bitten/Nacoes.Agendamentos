using Microsoft.EntityFrameworkCore;
using Application.Shared.Contexts;
using Application.Shared.Ports.Notifications;
using Domain.Shared.Events;
using Domain.Historicos.Interfaces;
using Domain.Usuarios.DomainEvents;

namespace Application.Entities.Usuarios.Commands.AceitarConvite;

internal sealed class UsuarioConviteAceitoDomainEventHandler(
    INacoesDbContext context,
    IHistoricoRegister historicoRegister,
    IEmailSenderFactory emailSenderFactory,
    ITemplateRenderer templateRenderer)
    : IDomainEventHandler<UsuarioConviteAceitoDomainEvent>, IDomainEvent
{
    private IEmailSender EmailSender => emailSenderFactory.Create();

    public async Task Handle(UsuarioConviteAceitoDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var usuarioConvite = await context.Convites
            .Where(x => x.Id == domainEvent.UsuarioConviteId)
            .Select(x => new
            {
                x.Nome,
                EmailEnviadoPor = x.EnviadoPor.Email
            }).SingleOrDefaultAsync(cancellationToken);
        if (usuarioConvite is null)
        {
            return;
        }

        await historicoRegister.AuditAsync(domainEvent.UsuarioConviteId, acao: "Convite aceito.");

        // var (title, html) = GetTemplate(usuarioConvite.Nome);
        // await EmailSender.SendAsync(usuarioConvite.EmailEnviadoPor, title, html);
    }

    private (string Title, string Html) GetTemplate(string nome)
    {
        var title = $"Convite aceito por {nome} - Igreja Nações";
        var html = templateRenderer.Render("ConviteAceito", new Dictionary<string, string>
        {
            { "NOME", nome }
        });

        return (title, html);
    }
}
