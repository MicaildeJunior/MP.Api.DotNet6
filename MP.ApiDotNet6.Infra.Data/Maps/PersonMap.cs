using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MP.ApiDotNet6.Domain.Entities;

namespace MP.ApiDotNet6.Infra.Data.Maps;

public class PersonMap : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("Pessoa");

        builder.HasKey(lbda => lbda.Id);

        builder.Property(lbda => lbda.Id)
            .HasColumnName("IdPessoa")
            .UseIdentityColumn(); // Chave primaria é unica e ela vai se incrementar

        builder.Property(lbda => lbda.Document)
            .HasColumnName("Documento");

        builder.Property(lbda => lbda.Name)
            .HasColumnName("Nome");

        builder.Property(lbda => lbda.Phone)
            .HasColumnName("Celular");

        // A Tabela Pessoa tem uma Lista de Compras(Purchases), onde tem uma Pessoa e sua ligação é feita através de IdPessoa                   
        // Uma Pessoa pode ter uma Lista de compras, mais uma compra é referente a uma pessoa.
        builder.HasMany(lbda => lbda.Purchases)
            .WithOne(lbda => lbda.Person)
            .HasForeignKey(lbda => lbda.PersonId);

        //  Relacionamento muitos pra um
        builder.HasMany(lbda => lbda.PersonImages)
               .WithOne(lbda => lbda.Person)
               .HasForeignKey(lbda => lbda.PersonId);
    }
}
