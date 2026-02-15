using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Voluntarios;
using Postgres.Abstracts;

namespace Postgres.Configurations.Voluntarios;

internal sealed class VoluntarioConfiguration : EntityIdConfiguration<Volunteer>
{
    public override void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        base.Configure(builder);

        builder.ToTable("voluntarios");

        builder.Property(v => v.Name)
            .HasColumnName("nome")
            .HasMaxLength(Volunteer.NameMaxLength);

        builder.OwnsOne(v => v.Email, emailBuilder =>
        {
            emailBuilder.Property(e => e.Address)
                        .HasColumnName("email");

            emailBuilder.Property(e => e.IsConfirmed)
                        .HasColumnName("email_confirmado");

            emailBuilder.Property(e => e.ConfirmationCode)
                        .HasColumnName("email_codigo_confirmacao");

            emailBuilder.Property(e => e.ConfirmationCodeExpiration)
                        .HasColumnName("email_data_expiracao_codigo_confirmacao");
        });

        builder.OwnsOne(v => v.PhoneNumber, celularBuilder =>
        {
            celularBuilder.Property(c => c.Number)
                .HasColumnName("numero");

            celularBuilder.Property(c => c.AreaCode)
                .HasColumnName("ddd");
        });

        builder.OwnsOne(v => v.Cpf, cpfBuilder =>
        {
            cpfBuilder.Property(c => c.Number)
                      .HasColumnName("cpf");
        });

        builder.OwnsOne(v => v.BirthDate, birthDateBuilder =>
        {
            birthDateBuilder.Property(d => d.Value)
                                 .HasColumnName("data_nascimento");
        });

        builder.Property(v => v.RegistrationOrigin)
            .HasColumnName("origem_cadastro");

        builder.HasMany(v => v.Ministries)
               .WithOne()
               .HasForeignKey("voluntario_id");
    }
}
