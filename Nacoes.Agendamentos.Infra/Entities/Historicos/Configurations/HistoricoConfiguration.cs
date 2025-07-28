using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nacoes.Agendamentos.Domain.Entities.Historicos;
using Nacoes.Agendamentos.Infra.Abstracts;

namespace Nacoes.Agendamentos.Infra.Entities.Historicos.Configurations;

internal sealed class HistoricoConfiguration : EntityIdConfiguration<Historico>
{
    public override void Configure(EntityTypeBuilder<Historico> builder)
    {
        base.Configure(builder);

        builder.Property(h => h.EntidadeId)
               .IsRequired(false);
        
        builder.Property(h => h.Data)
               .IsRequired();
        
        builder.Property(h => h.UsuarioId)
               .HasColumnType("uuid")
               .IsRequired(false);
        
        builder.Property(h => h.Acao)
               .IsRequired();
        
        builder.Property(h => h.TipoAcao)
               .IsRequired()
               .HasConversion<string>();
        
        builder.Property(h => h.UsuarioAcao)
               .IsRequired();
        
        builder.Property(h => h.Detalhes)
               .IsRequired(false);

        builder.Ignore(h => h.Inativo);
        builder.Ignore(h => h.DataCriacao);
    }
}