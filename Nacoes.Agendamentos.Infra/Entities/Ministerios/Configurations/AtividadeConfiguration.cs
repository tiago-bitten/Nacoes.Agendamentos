using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Infra.Abstracts;

namespace Nacoes.Agendamentos.Infra.Entities.Ministerios.Configurations;

public class AtividadeConfiguration : EntityIdConfiguration<Atividade>
{
    public override void Configure(EntityTypeBuilder<Atividade> builder)
    {
        base.Configure(builder);

        builder.Property(a => a.Nome);

        builder.Property(a => a.Descricao);
    }
}
