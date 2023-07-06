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

        builder.HasKey(p => p.Id).HasName("PkOrderId");
        builder
            .Property(p => p.Id)
            .HasColumnName("Id")
            .HasColumnType("varchar(36)")
            .IsRequired();

        builder.HasOne(p => p.Cart).WithMany(p => p.Orders).HasForeignKey(p => p.CartId);
    }
}
