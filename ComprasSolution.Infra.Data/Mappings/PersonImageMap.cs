using ComprasSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComprasSolution.Infra.Data.Mappings
{
    public class PersonImageMap : BaseMap<PersonImage>
    {
        public override void Configure(EntityTypeBuilder<PersonImage> builder)
        {
            base.Configure(builder);

            builder.ToTable("pessoaimagem");

            builder.Property(x => x.Id)
                .HasColumnName("idimagem");

            builder.Property(x => x.PersonId)
                .HasColumnName("idpessoa");

            builder.Property(x => x.ImageUri)
                .HasColumnName("imagemurl");

            builder.Property(x => x.ImageBase)
                .HasColumnName("imagembase");

            builder.HasOne(x => x.Person)
                .WithMany(x => x.PersonImages);
        }
    }
}
