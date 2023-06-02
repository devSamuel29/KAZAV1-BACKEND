using Microsoft.EntityFrameworkCore;

using kazariobranco_backend.Models;

namespace kazariobranco_backend.Mapping;

public class CartMap : IEntityTypeConfiguration<CartModel>
{
    public void Configure(
        Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CartModel> builder
    )
    {
        builder
            .Property(p => p.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .UseIdentityColumn()
            .IsRequired();
    }
}
