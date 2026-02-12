using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Ministerios;
using Postgres.Abstracts;

namespace Postgres.Configurations.Ministerios;

internal class AtividadeConfiguration : EntityIdConfiguration<Atividade>
{
    public override void Configure(EntityTypeBuilder<Atividade> builder)
    {
        base.Configure(builder);

        builder.Property(a => a.Nome);

        builder.Property(a => a.Descricao);

        builder.HasOne(a => a.Ministerio)
            .WithMany(m => m.Atividades)
            .HasForeignKey(a => a.MinisterioId);
    }
}
