using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Infra.Abstracts;

namespace Nacoes.Agendamentos.Infra.Entities.Usuarios.Configurations;

public class UsuarioAprovacaoConfiguration : EntityIdConfiguration<UsuarioAprovacao>
{
    public override void Configure(EntityTypeBuilder<UsuarioAprovacao> builder)
    {
        base.Configure(builder);

        builder.Property(u => u.Status)
               .HasConversion<string>();

        builder.Property(u => u.DataAprovacao);

        builder.HasOne(u => u.Aprovador)
               .WithMany()
               .HasForeignKey("usuario_aprovador_id");
    }
}
