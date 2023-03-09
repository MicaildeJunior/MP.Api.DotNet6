using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MP.ApiDotNet6.Domain.Entities;

namespace MP.ApiDotNet6.Infra.Data.Maps;

public class PersonImageMap : IEntityTypeConfiguration<PersonImage>
{
    public void Configure(EntityTypeBuilder<PersonImage> builder)
    {
        builder.ToTable("PessoaImagem");
        builder.HasKey(lbda => lbda.Id);

        builder.Property(lbda => lbda.Id)
               .HasColumnName("IdImagem")
               .UseIdentityColumn();

        builder.Property(lbda => lbda.PersonId)
               .HasColumnName("IdPessoa");

        builder.Property(lbda => lbda.ImageUri)
               .HasColumnName("ImagemUrl");

        builder.Property(lbda => lbda.ImageBase)
               .HasColumnName("ImagemBase");

        // Relacionamento um pra muitos
        builder.HasOne(lbda => lbda.Person)
               .WithMany(lbda => lbda.PersonImages);
    }
}
