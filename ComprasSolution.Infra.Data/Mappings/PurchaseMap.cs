using ComprasSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComprasSolution.Infra.Data.Mappings
{
    public class PurchaseMap : BaseMap<Purchase>
    {
        public override void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("compra");

            builder.Property(p => p.Id)
                   .HasColumnName("idcompra")
                   .UseIdentityColumn();

            builder.Property(x => x.PersonId)
                    .HasColumnName("idpessoa");

            builder.Property(x => x.ProductId)
                    .HasColumnName("idproduto");

            builder.Property(x => x.Date)
                    .HasColumnName("datacompra");

            builder.HasOne(x => x.Person)
                    .WithMany(x => x.Purchases);

            builder.HasOne(x => x.Product)
                    .WithMany(x => x.Purcheses);

        }
    }
}
