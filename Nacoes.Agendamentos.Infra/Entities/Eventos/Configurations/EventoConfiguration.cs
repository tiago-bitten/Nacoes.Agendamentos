using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nacoes.Agendamentos.Domain.Entities.Eventos;
using Nacoes.Agendamentos.Infra.Abstracts;

namespace Nacoes.Agendamentos.Infra.Entities.Eventos.Configurations;
internal class EventoConfiguration : EntityIdConfiguration<Evento>
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
    }
}
