using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Application.Abstracts.Notifications;
using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Entities.Historicos.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.DomainEvents;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.Interfaces;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Commands.AceitarConvite;

internal sealed class UsuarioConviteAceitoDomainEventHandler(IHistoricoRegister historicoRegister,
                                                             IEmailSenderFactory emailSenderFactory,
                                                             IUsuarioConviteRepository usuarioConviteRepository,
                                                             ITemplateRenderer templateRenderer)
    : IDomainEventHandler<UsuarioConviteAceitoDomainEvent>, IDomainEvent
{
    private IEmailSender EmailSender => emailSenderFactory.Create();
    
    public async Task Handle(UsuarioConviteAceitoDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var usuarioConvite = await usuarioConviteRepository.GetByIdToProject(domainEvent.UsuarioConviteId)
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
        
        var (title, html) = GetTemplate(usuarioConvite.Nome);
        await EmailSender.SendAsync(usuarioConvite.EmailEnviadoPor, title, html);
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