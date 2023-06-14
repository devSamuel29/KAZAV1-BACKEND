using Microsoft.EntityFrameworkCore;

using kazariobranco_backend.Models;

namespace kazariobranco_backend.Mapping;

public class AddressMap : IEntityTypeConfiguration<AddressModel>
{
    public void Configure(
        Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<AddressModel> builder
    )
    {
        builder.ToTable("Address");

        builder.HasKey(p => p.Id).HasName("PkAddressId");
        builder
            .Property(p => p.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .UseIdentityColumn()
            .IsRequired();

        builder
            .Property(p => p.Address)
            .HasColumnName("Adress")
            .HasColumnType("varchar(40)")
            .IsRequired();

        builder.Property(p => p.Number).HasColumnName("Number").HasColumnType("int").IsRequired();

        builder
            .Property(p => p.District)
            .HasColumnName("District")
            .HasColumnType("varchar(30)")
            .IsRequired();

        builder
            .Property(p => p.City)
            .HasColumnName("City")
            .HasColumnType("varchar(20)")
            .IsRequired();

        builder
            .Property(p => p.State)
            .HasColumnName("State")
            .HasColumnType("varchar(20)")
            .IsRequired();

        builder.Property(p => p.ZipCode).HasColumnName("ZipCode").HasColumnType("int").IsRequired();

        builder.Property(p => p.UserId).HasColumnName("UserId").HasColumnType("int").IsRequired();

        builder
            .HasOne(p => p.User)
            .WithMany(p => p.Addresses)
            .HasForeignKey(p => p.UserId)
            .HasConstraintName("FkAddressUserId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
