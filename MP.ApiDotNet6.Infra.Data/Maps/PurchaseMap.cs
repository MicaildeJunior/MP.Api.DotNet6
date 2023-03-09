using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MP.ApiDotNet6.Domain.Entities;

namespace MP.ApiDotNet6.Infra.Data.Maps;

public class PurchaseMap : IEntityTypeConfiguration<Purchase>
{
    public void Configure(EntityTypeBuilder<Purchase> builder)
    {
        builder.ToTable("Compra");
        builder.HasKey(lbda => lbda.Id);

        builder.Property(lbda => lbda.Id)
            .HasColumnName("IdCompra")
            .UseIdentityColumn();

        builder.Property(lbda => lbda.PersonId)
            .HasColumnName("IdPessoa");

        builder.Property(lbda => lbda.ProductId)
            .HasColumnName("IdProduto");

        builder.Property(lbda => lbda.Date)
            //HasColumnType("Date") no PostGre deu erro pq faltou isso
            .HasColumnName("DataCompra");

        // Uma pessoa N compras
        builder.HasOne(lbda => lbda.Person)
            .WithMany(lbda => lbda.Purchases);

        // Um produto para N compras
        builder.HasOne(lbda => lbda.Product)
            .WithMany(lbda => lbda.Purchases);
    }
}
