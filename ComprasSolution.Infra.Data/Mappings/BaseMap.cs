using ComprasSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComprasSolution.Infra.Data.Mappings
{
    public class BaseMap<T> : IEntityTypeConfiguration<T> where T : Base
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(p => p.Id);
        }
    }
}
