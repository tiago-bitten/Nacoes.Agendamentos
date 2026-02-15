using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Eventos;
using Postgres.Abstracts;

namespace Postgres.Configurations.Eventos;

internal sealed class EventoConfiguration : EntityIdConfiguration<Event>
{
    public override void Configure(EntityTypeBuilder<Event> builder)
    {
        base.Configure(builder);

        builder.ToTable("eventos");

        builder.Property(a => a.Description)
            .HasColumnName("descricao")
            .HasMaxLength(Event.DescriptionMaxLength)
            .IsRequired();

        builder.OwnsOne(a => a.Schedule, scheduleBuilder =>
        {
            scheduleBuilder.Property(h => h.StartDate)
                .HasColumnName("data_inicial")
                .IsRequired();

            scheduleBuilder.Property(h => h.EndDate)
                .HasColumnName("data_final")
                .IsRequired();
        });

        builder.Property(a => a.MaxReservationCount)
            .HasColumnName("quantidade_maxima_reservas");

        builder.Property(a => a.ReservationCount)
            .HasColumnName("quantidade_reservas")
            .IsRequired();

        builder.Property(a => a.Status)
            .HasColumnName("status")
            .IsRequired();

        builder.OwnsOne(a => a.Recurrence, recurrenceBuilder =>
        {
            recurrenceBuilder.Property(r => r.Type)
                .IsRequired()
                .HasColumnName("tipo_recorrencia");

            recurrenceBuilder.Property(r => r.Interval)
                .HasColumnName("intervalo_recorrencia");

            recurrenceBuilder.Property(r => r.EndDate)
                .HasColumnName("data_final_recorrencia");
        });
    }
}
