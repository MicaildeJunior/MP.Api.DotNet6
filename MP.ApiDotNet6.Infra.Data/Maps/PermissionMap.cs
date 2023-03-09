using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MP.ApiDotNet6.Domain.Entities;

namespace MP.ApiDotNet6.Infra.Data.Maps;

public class PermissionMap : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("Permissao");
        builder.HasKey(lbda => lbda.Id);

        builder.Property(lbda => lbda.Id)
               .HasColumnName("IdPermissao")
               .UseIdentityColumn();

        builder.Property(lbda => lbda.VisualName)
               .HasColumnName("NomeVisual");

        builder.Property(lbda => lbda.PermissionName)
               .HasColumnName("NomePermissao");

        // Relacionamento N pra 1
        builder.HasMany(lbda => lbda.UserPermissions)
               .WithOne(lbda => lbda.Permission)
               .HasForeignKey(lbda => lbda.PermissionId);             
    }
}
