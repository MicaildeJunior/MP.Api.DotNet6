using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MP.ApiDotNet6.Domain.Entities;

namespace MP.ApiDotNet6.Infra.Data.Maps;

public class UserPermissionMap : IEntityTypeConfiguration<UserPermission>
{
    public void Configure(EntityTypeBuilder<UserPermission> builder)
    {
        builder.ToTable("PermissaoUsuario");
        builder.HasKey(lbda => lbda.Id);

        builder.Property(lbda => lbda.Id)
               .HasColumnName("IdPermissaoUsuario")
               .UseIdentityColumn();

        builder.Property(lbda => lbda.PermissionId)
               .HasColumnName("IdPermissao");

        builder.Property(lbda => lbda.UserId)
               .HasColumnName("IdUsuario");

        // Relacionamento 1 pra N
        builder.HasOne(lbda => lbda.Permission)
               .WithMany(lbda => lbda.UserPermissions);

        builder.HasOne(lbda => lbda.User)
                .WithMany(lbda => lbda.UserPermissions);
    }
}
