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
    DbSet<User> Users { get; set; }
    DbSet<UserInvitation> Invitations { get; set; }
    DbSet<Event> Events { get; set; }
    DbSet<Reservation> Reservations { get; set; }
    DbSet<Volunteer> Volunteers { get; set; }
    DbSet<VolunteerMinistry> VolunteerMinistries { get; set; }
    DbSet<AuditLog> AuditLogs { get; set; }
    DbSet<Ministry> Ministries { get; set; }
    DbSet<Activity> Activities { get; set; }
    DbSet<UserInvitationMinistry> InvitationMinistries { get; set; }

    Task<int> SaveChangesAsync(CancellationToken ct = default);
    Task PublishDomainEventsAsync(CancellationToken ct = default);
}
