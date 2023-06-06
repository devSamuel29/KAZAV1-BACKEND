using Microsoft.EntityFrameworkCore;

using kazariobranco_backend.Models;

namespace kazariobranco_backend.Mapping;

public class OrderMap : IEntityTypeConfiguration<OrderModel>
{
    public void Configure(
        Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<OrderModel> builder
    )
    {
        builder.ToTable("Orders");

        builder.HasKey(p => p.Id).HasName("PkAOrderId");
        builder
            .Property(p => p.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .UseIdentityColumn()
            .IsRequired();

        builder.HasOne(p => p.Cart).WithMany(p => p.Orders).HasForeignKey(p => p.CartId);

        builder
            .HasMany(p => p.Products)
            .WithMany(p => p.Orders)
            .UsingEntity<OrderProductModel>(
                l => l.HasOne(p => p.Product).WithMany(p => p.OrderProducts),
                r => r.HasOne(p => p.Order).WithMany(e => e.OrderProducts)
            );
    }
}
