using ComprasSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComprasSolution.Infra.Data.Mappings
{
    public class PersonMap : BaseMap<Person>
    {
        public override void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("pessoa");

            builder.Property(p => p.Id)
                   .HasColumnName("idpessoa")
                   .UseIdentityColumn();

            builder.Property(p => p.Document)
                   .HasColumnName("documento");

            builder.Property(p => p.Name)
                   .HasColumnName("nome");

            builder.Property(p => p.Phone)
                   .HasColumnName("celular");

            builder.HasMany(p => p.Purchases)
                   .WithOne(p => p.Person)
                   .HasForeignKey(p => p.PersonId);

            builder.HasMany(x => x.PersonImages)
                    .WithOne(x => x.Person)
                    .HasForeignKey(x => x.PersonId);
        }
    }
}
