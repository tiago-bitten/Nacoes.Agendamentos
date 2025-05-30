using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Infra.Abstracts;

namespace Nacoes.Agendamentos.Infra.Entities.Usuarios.Configurations;

public sealed class UsuarioAprovacaoMinisterioConfiguration : EntityIdConfiguration<UsuarioAprovacaoMinisterio>
{
    public override void Configure(EntityTypeBuilder<UsuarioAprovacaoMinisterio> builder)
    {
        base.Configure(builder);

        builder.Property(u => u.MinisterioId);

        builder.Property(u => u.Aprovado);
    }
}
