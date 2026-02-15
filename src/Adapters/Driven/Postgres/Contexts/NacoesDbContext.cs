using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Application.Shared.Contexts;
using Domain.Shared.Entities;
using Domain.Eventos;
using Domain.Eventos.Reservas;
using Domain.Historicos;
using Domain.Ministerios;
using Domain.Usuarios;
using Domain.Voluntarios;
using Postgres.DomainEvents;
using Postgres.Extensions;

namespace Postgres.Contexts;

internal sealed class NacoesDbContext(
    DbContextOptions<NacoesDbContext> options,
    IDomainEventsDispatcher domainEventsDispatcher)
    : DbContext(options), INacoesDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserInvitation> Invitations { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Volunteer> Volunteers { get; set; }
    public DbSet<VolunteerMinistry> VolunteerMinistries { get; set; }
    public DbSet<Ministry> Ministries { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }
    public DbSet<UserInvitationMinistry> InvitationMinistries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NacoesDbContext).Assembly);
        modelBuilder.ApplyValueObjectConverters();
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        var entityEntries = ChangeTracker
            .Entries()
            .Where(x => x.State is EntityState.Added or EntityState.Modified);

        foreach (var entity in entityEntries)
        {
            if (entity.State is EntityState.Modified)
            {
                SaveModified(entity);
            }
        }
        var result = await base.SaveChangesAsync(ct);

        await PublishDomainEventsAsync(ct);

        return result;
    }

    private const string CreatedAtPropertyName = "CreatedAt";
    private static void SaveModified(EntityEntry entityEntry)
    {
        entityEntry.Property(CreatedAtPropertyName).IsModified = false;
    }

    public Task PublishDomainEventsAsync(CancellationToken ct = default)
    {
        var domainEvents = ChangeTracker
            .Entries<IEntity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.DomainEvents;
                entity.ClearDomainEvents();
                return domainEvents;
            }).ToList();

        return domainEventsDispatcher.DispatchAsync(domainEvents, ct);
    }
}
