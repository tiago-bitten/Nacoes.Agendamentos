using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Domain.Eventos;
using Domain.Eventos.Reservas;
using Domain.Historicos;
using Domain.Ministerios;
using Domain.Usuarios;
using Domain.Voluntarios;

namespace Application.Shared.Contexts;

public interface INacoesDbContext
{
    DbSet<Usuario> Usuarios { get; set; }
    DbSet<UsuarioConvite> Convites { get; set; }
    DbSet<Evento> Eventos { get; set; }
    DbSet<Reserva> Agendamentos { get; set; }
    DbSet<Voluntario> Voluntarios { get; set; }
    DbSet<VoluntarioMinisterio> VoluntariosMinisterios { get; set; }
    DbSet<Historico> Historicos { get; set; }
    DbSet<Ministerio> Ministerios { get; set; }
    DbSet<Atividade> Atividades { get; set; }
    DbSet<UsuarioConviteMinisterio> ConvitesMinisterios { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task PublishDomainEventsAsync(CancellationToken cancellationToken = default);
}
