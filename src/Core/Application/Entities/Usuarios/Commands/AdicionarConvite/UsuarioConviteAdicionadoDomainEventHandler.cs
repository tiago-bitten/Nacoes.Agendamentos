using Microsoft.EntityFrameworkCore;
using Application.Shared.Contexts;
using Application.Shared.Ports.Notifications;
using Domain.Shared.Events;
using Domain.Historicos.Interfaces;
using Domain.Usuarios;
using Domain.Usuarios.DomainEvents;

namespace Application.Entities.Usuarios.Commands.AdicionarConvite;

internal sealed class UsuarioConviteAdicionadoDomainEventHandler(INacoesDbContext context,
                                                                 IEmailSenderFactory emailSenderFactory,
                                                                 ITemplateRenderer templateRenderer,
                                                                 IHistoricoRegister historicoRegister)
    : IDomainEventHandler<UsuarioConviteAdicionadoDomainEvent>
{
    private IEmailSender EmailSender => emailSenderFactory.Create();

    public async Task Handle(UsuarioConviteAdicionadoDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        await historicoRegister.AuditAsync(domainEvent.UsuarioConviteId, acao: "Convite gerado.");
        var usuarioConvite = await context.Convites
            .AsNoTracking()
            .Include(x => x.EnviadoPor)
            .SingleOrDefaultAsync(x => x.Id == domainEvent.UsuarioConviteId, cancellationToken);
        try
        {
            if (usuarioConvite is null)
            {
                return;
            }
            await historicoRegister.AuditAsync(usuarioConvite, acao: "Convite enviado por e-mail.");
            var (email, title, html) = GetTemplate(usuarioConvite);
            await EmailSender.SendAsync(email, title, html);

            await historicoRegister.AuditAsync(usuarioConvite, acao: "Convite entregue na caixa de e-mail.");
        }
        catch (Exception ex)
        {
            // TODO: historicoRegister tem que ser mais flexivel, não precisa receber a entidae, pode ser só id
            await historicoRegister.AuditAsync(usuarioConvite!, acao: "Ocorreu um erro durante o envio do convite por e-mail.", detalhes: ex.Message);
        }
    }

    private (string Email, string Title, string Html) GetTemplate(UsuarioConvite usuarioConvite)
    {
        var title = "Convite - Igreja Nações";
        var html = templateRenderer.Render("ConviteUsuario", new Dictionary<string, string>()
        {
            { "NOME_CONVIDADO", usuarioConvite.Nome },
            { "NOME_USUARIO", usuarioConvite.EnviadoPor.Nome },
            { "LINK", "http://localhost:4200/convites" }
        });

        return (usuarioConvite.Email.Address, title, html);
    }
}
