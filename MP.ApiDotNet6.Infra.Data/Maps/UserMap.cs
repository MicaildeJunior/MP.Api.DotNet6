using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MP.ApiDotNet6.Domain.Entities;

namespace MP.ApiDotNet6.Infra.Data.Maps;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Usuario");
        builder.HasKey(lbda => lbda.Id);

        builder.Property(lbda => lbda.Id)
            .HasColumnName("IdUsuario");

        builder.Property(lbda => lbda.Email)
            .HasColumnName("Email");

        builder.Property(lbda => lbda.Password)
            .HasColumnName("Senha");

        builder.HasMany(lbda => lbda.UserPermissions)
               .WithOne(lbda => lbda.User)
               .HasForeignKey(lbda => lbda.UserId);
    }
}
