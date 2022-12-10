using ComprasSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComprasSolution.Infra.Data.Mappings
{
    public class ProductMap : BaseMap<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("produto");

            builder.Property(p => p.Id)
                   .HasColumnName("idproduto")
                   .UseIdentityColumn();

            builder.Property(x => x.CodErp)
                    .HasColumnName("coderp");

            builder.Property(x => x.Name)
                    .HasColumnName("nome");

            builder.Property(x => x.Price)
                    .HasColumnName("preco");

            builder.HasMany(x => x.Purcheses)
                    .WithOne(p => p.Product)
                    .HasForeignKey(p => p.ProductId);
        }
    }
}
