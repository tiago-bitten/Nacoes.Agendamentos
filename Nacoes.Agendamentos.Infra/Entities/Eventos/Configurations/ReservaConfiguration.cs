using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nacoes.Agendamentos.Domain.Entities.Eventos;
using Nacoes.Agendamentos.Domain.Entities.Eventos.Reservas;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios;
using Nacoes.Agendamentos.Infra.Abstracts;

namespace Nacoes.Agendamentos.Infra.Entities.Eventos.Configurations;

internal class ReservaConfiguration : EntityIdConfiguration<Reserva>
{
    public override void Configure(EntityTypeBuilder<Reserva> builder)
    {
        base.Configure(builder);
        
        builder.Property(a => a.EventoId)
            .IsRequired();

        builder.Property(a => a.VoluntarioMinisterioId);

        builder.Property(a => a.AtividadeId);

        builder.Property(a => a.Status)
            .HasConversion<string>();

        builder.Property(a => a.Origem)
            .HasConversion<string>();

        builder.HasOne(x => x.Evento)
            .WithMany(x => x.Agendamentos)
            .HasForeignKey(a => a.EventoId);

        builder.HasOne<VoluntarioMinisterio>()
            .WithMany()
            .HasForeignKey(a => a.VoluntarioMinisterioId);

        builder.HasOne<Atividade>()
            .WithMany()
            .HasForeignKey(a => a.AtividadeId);
    }
}
