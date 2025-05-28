using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nacoes.Agendamentos.Domain.Entities.Agendas;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.Entities.VoluntariosMinisterios;
using Nacoes.Agendamentos.Infra.Abstracts;

namespace Nacoes.Agendamentos.Infra.Entities.Agendas.Configurations;

public class AgendamentoConfiguration : EntityIdConfiguration<Agendamento>
{
    public override void Configure(EntityTypeBuilder<Agendamento> builder)
    {
        base.Configure(builder);

        builder.Property(a => a.VoluntarioMinisterioId);

        builder.Property(a => a.AtividadeId);

        builder.Property(a => a.Status)
               .HasConversion<string>();

        builder.Property(a => a.Origem)
               .HasConversion<string>();

        builder.HasOne<VoluntarioMinisterio>()
               .WithMany()
               .HasForeignKey(a => a.VoluntarioMinisterioId);

        builder.HasOne<Atividade>()
               .WithMany()
               .HasForeignKey(a => a.AtividadeId);
    }
}
