using ComprasSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComprasSolution.Infra.Data.Mappings
{
    public class UserMap : BaseMap<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.ToTable("usuario");

            builder.Property(x => x.Id)
                .HasColumnName("idusuario");

            builder.Property(x => x.Email)
                .HasColumnName("email");

            builder.Property(x => x.Password)
                .HasColumnName("senha");
        }
    }
}
