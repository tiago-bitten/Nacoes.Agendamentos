using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nacoes.Agendamentos.Domain.Entities.Agendas;
using Nacoes.Agendamentos.Infra.Abstracts;

namespace Nacoes.Agendamentos.Infra.Entities.Agendas.Configurations;
public class AgendaConfiguration : EntityIdConfiguration<Agenda>
{
    public override void Configure(EntityTypeBuilder<Agenda> builder)
    {
        base.Configure(builder);

        builder.Property(a => a.Descricao);

        builder.OwnsOne(a => a.Horario, horarioBuilder =>
        {
            horarioBuilder.Property(h => h.DataInicial);

            horarioBuilder.Property(h => h.DataFinal);
        });

        // Shadow Property: Não existe campo agenda em agendamentos
        builder.HasMany(a => a.Agendamentos)
               .WithOne()
               .HasForeignKey("agenda_id");
    }
}
