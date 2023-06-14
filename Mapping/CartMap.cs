using Microsoft.EntityFrameworkCore;

using kazariobranco_backend.Models;

namespace kazariobranco_backend.Mapping;

public class CartMap : IEntityTypeConfiguration<CartModel>
{
    public void Configure(
        Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CartModel> builder
    )
    {
        builder.HasKey(p => p.Id).HasName("PkCartId");
        builder
            .Property(p => p.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .UseIdentityColumn()
            .IsRequired();

        builder
            .Property(p => p.UserId)
            .HasColumnName("UserId")
            .HasColumnType("int")
            .IsRequired();
        builder
            .HasOne(p => p.User)
            .WithOne(p => p.Cart)
            .HasConstraintName("FkUserId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
