using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MP.ApiDotNet6.Domain.Entities;

namespace MP.ApiDotNet6.Infra.Data.Maps;

public class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Produto");
        builder.HasKey(lbda => lbda.Id);

        builder.Property(lbda => lbda.Id)
            .HasColumnName("IdProduto")
            .UseIdentityColumn();

        builder.Property(lbda => lbda.Name)
               .HasColumnName("Nome");

        builder.Property(lbda => lbda.CodeErp)
               .HasColumnName("CodErp");

        builder.Property(lbda => lbda.Price)
               .HasColumnName("Preco");

        // Um produto para N compras
        builder.HasMany(lbda => lbda.Purchases)
               .WithOne(lbda => lbda.Product)
               .HasForeignKey(lbda => lbda.ProductId);
    }
}
