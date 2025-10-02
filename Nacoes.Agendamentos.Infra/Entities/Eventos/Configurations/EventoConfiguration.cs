using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nacoes.Agendamentos.Domain.Entities.Eventos;
using Nacoes.Agendamentos.Infra.Abstracts;

namespace Nacoes.Agendamentos.Infra.Entities.Eventos.Configurations;

internal sealed class EventoConfiguration : EntityIdConfiguration<Evento>
{
    public override void Configure(EntityTypeBuilder<Evento> builder)
    {
        base.Configure(builder);

        builder.Property(a => a.Descricao);

        builder.OwnsOne(a => a.Horario, horarioBuilder =>
        {
            horarioBuilder.Property(h => h.DataInicial);

            horarioBuilder.Property(h => h.DataFinal);
        });
        
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
