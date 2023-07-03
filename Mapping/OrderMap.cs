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
            .HasColumnType("int")
            .UseIdentityColumn()
            .IsRequired();


    }
}
