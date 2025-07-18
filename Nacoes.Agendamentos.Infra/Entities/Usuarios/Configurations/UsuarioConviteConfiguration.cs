﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Infra.Abstracts;

namespace Nacoes.Agendamentos.Infra.Entities.Usuarios.Configurations;

public sealed class UsuarioConviteConfiguration : EntityIdConfiguration<UsuarioConvite>
{
    public override void Configure(EntityTypeBuilder<UsuarioConvite> builder)
    {
        base.Configure(builder);

        builder.Property(u => u.Nome);
        
        builder.OwnsOne(u => u.Email, emailBuilder =>
        {
            emailBuilder.Property(e => e.Address)
                        .HasColumnName("email");
            
            emailBuilder.Ignore(e => e.ConfirmationCode);
            emailBuilder.Ignore(e => e.ConfirmationCodeExpiration);
            emailBuilder.Ignore(e => e.IsConfirmed);
        });

        builder.Property(u => u.EnviadoPorId);

        builder.Property(u => u.EnviadoParaId);
        
        builder.Property(u => u.Status)
               .HasConversion<string>();

        builder.Property(u => u.DataExpiracao);
        
        builder.Property(u => u.Token);
        
        builder.HasOne(u => u.EnviadoPor)
               .WithMany()
               .HasForeignKey(u => u.EnviadoPorId);
        
        builder.HasOne(u => u.EnviadoPara)
               .WithMany()
               .HasForeignKey(u => u.EnviadoParaId);
    }
}