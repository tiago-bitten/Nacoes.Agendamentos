using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Eventos;
using Postgres.Abstracts;

namespace Postgres.Configurations.Eventos;

internal sealed class EventoConfiguration : EntityIdConfiguration<Evento>
{
    public override void Configure(EntityTypeBuilder<Evento> builder)
    {
        base.Configure(builder);

        builder.Property(a => a.Descricao)
            .IsRequired();

        builder.OwnsOne(a => a.Horario, horarioBuilder =>
        {
            horarioBuilder.Property(h => h.DataInicial)
                .IsRequired();

            horarioBuilder.Property(h => h.DataFinal)
                .IsRequired();
        });

        builder.Property(a => a.QuantidadeMaximaReservas);

        builder.Property(a => a.QuantidadeReservas)
            .IsRequired();

        builder.Property(a => a.Status)
            .IsRequired();

        builder.OwnsOne(a => a.Recorrencia, recorrenciaBuilder =>
        {
            recorrenciaBuilder.Property(r => r.Tipo)
                .IsRequired()
                .HasColumnName("tipo_recorrencia");

            recorrenciaBuilder.Property(r => r.Intervalo)
                .HasColumnName("intervalo_recorrencia");

            recorrenciaBuilder.Property(r => r.DataFinal)
                .HasColumnName("data_final_recorrencia");
        });
    }
}
