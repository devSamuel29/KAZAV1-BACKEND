using Microsoft.EntityFrameworkCore;

using kazariobranco_backend.Models;

namespace kazariobranco_backend.Mapping;

public class ProductMap : IEntityTypeConfiguration<ProductModel>
{
    public void Configure(
        Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProductModel> builder
    )
    {
        builder
            .Property(p => p.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .UseIdentityColumn()
            .IsRequired();

        builder.HasOne(p => p.Cart).WithMany(p => p.Products).HasForeignKey(p => p.CartId);
    }
}
