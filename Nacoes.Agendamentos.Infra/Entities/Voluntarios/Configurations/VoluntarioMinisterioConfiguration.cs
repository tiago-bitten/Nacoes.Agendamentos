using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios;
using Nacoes.Agendamentos.Infra.Abstracts;

namespace Nacoes.Agendamentos.Infra.Entities.Voluntarios.Configurations;

public class VoluntarioMinisterioConfiguration : EntityIdConfiguration<VoluntarioMinisterio>
{
    public override void Configure(EntityTypeBuilder<VoluntarioMinisterio> builder)
    {
        base.Configure(builder);

        builder.Property(v => v.VoluntarioId);

        builder.Property(v => v.MinisterioId);

        builder.Property(v => v.Status)
               .HasConversion<string>();
    }
}
