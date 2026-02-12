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

internal class NacoesDbContext(
    DbContextOptions<NacoesDbContext> options,
    IDomainEventsDispatcher domainEventsDispatcher)
    : DbContext(options), INacoesDbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<UsuarioConvite> Convites { get; set; }
    public DbSet<Evento> Eventos { get; set; }
    public DbSet<Reserva> Agendamentos { get; set; }
    public DbSet<Voluntario> Voluntarios { get; set; }
    public DbSet<VoluntarioMinisterio> VoluntariosMinisterios { get; set; }
    public DbSet<Ministerio> Ministerios { get; set; }
    public DbSet<Atividade> Atividades { get; set; }
    public DbSet<Historico> Historicos { get; set; }
    public DbSet<UsuarioConviteMinisterio> ConvitesMinisterios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NacoesDbContext).Assembly);
        modelBuilder.ApplyValueObjectConverters();
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entityEntries = ChangeTracker
            .Entries()
            .Where(x => x.State is EntityState.Added or EntityState.Modified);

        foreach (var entity in entityEntries)
        {
            switch (entity.State)
            {
                case EntityState.Added:
                    SaveAdded(entity);
                    break;

                case EntityState.Modified:
                    SaveModified(entity);
                    break;
            }
        }
        var result = await base.SaveChangesAsync(cancellationToken);

        await PublishDomainEventsAsync(cancellationToken);

        return result;
    }

    private static void SaveAdded(EntityEntry entityEntry)
    {
        var newEntity = entityEntry.Entity;
        var type = newEntity.GetType();

        Console.WriteLine(type.Name + " criado.");
    }

    private const string CreatedAtPropertyName = "CreatedAt";
    private static void SaveModified(EntityEntry entityEntry)
    {
        entityEntry.Property(CreatedAtPropertyName).IsModified = false;
    }

    public Task PublishDomainEventsAsync(CancellationToken cancellationToken = default)
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

        return domainEventsDispatcher.DispatchAsync(domainEvents, cancellationToken);
    }
}
