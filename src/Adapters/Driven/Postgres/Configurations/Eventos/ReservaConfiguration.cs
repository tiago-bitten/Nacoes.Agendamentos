using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Eventos;
using Domain.Eventos.Reservas;
using Domain.Ministerios;
using Domain.Voluntarios;
using Postgres.Abstracts;

namespace Postgres.Configurations.Eventos;

internal sealed class ReservaConfiguration : EntityIdConfiguration<Reservation>
{
    public override void Configure(EntityTypeBuilder<Reservation> builder)
    {
        base.Configure(builder);

        builder.ToTable("reservas");

        builder.Property(a => a.EventId)
            .HasColumnName("evento_id")
            .IsRequired();

        builder.Property(a => a.VolunteerMinistryId)
            .HasColumnName("voluntario_ministerio_id");

        builder.Property(a => a.ActivityId)
            .HasColumnName("atividade_id");

        builder.Property(a => a.Status)
            .HasColumnName("status")
            .HasConversion(
                v => v == EReservationStatus.AwaitingConfirmation ? "AguardandoConfirmacao"
                    : v == EReservationStatus.Confirmed ? "Confirmado"
                    : "Cancelado",
                v => v == "AguardandoConfirmacao" ? EReservationStatus.AwaitingConfirmation
                    : v == "Confirmado" ? EReservationStatus.Confirmed
                    : EReservationStatus.Cancelled);

        builder.Property(a => a.Origin)
            .HasColumnName("origem")
            .HasConversion(
                v => v == EReservationOrigin.Manual ? "Manual"
                    : v == EReservationOrigin.Automatic ? "Automatico"
                    : "Escala",
                v => v == "Manual" ? EReservationOrigin.Manual
                    : v == "Automatico" ? EReservationOrigin.Automatic
                    : EReservationOrigin.Roster);

        builder.HasOne(x => x.Event)
            .WithMany(x => x.Reservations)
            .HasForeignKey(a => a.EventId);

        builder.HasOne<VolunteerMinistry>()
            .WithMany()
            .HasForeignKey(a => a.VolunteerMinistryId);

        builder.HasOne<Activity>()
            .WithMany()
            .HasForeignKey(a => a.ActivityId);
    }
}
