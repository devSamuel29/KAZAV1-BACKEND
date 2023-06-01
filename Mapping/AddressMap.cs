using Microsoft.EntityFrameworkCore;

using kazariobranco_backend.Models;

namespace kazariobranco_backend.Mapping;

public class AddressMap : IEntityTypeConfiguration<AddressModel>
{
    public void Configure(
        Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<AddressModel> builder
    )
    {
        builder.ToTable("Addresses");

        builder.HasKey(u => u.Id).HasName("PkAddressId");
        builder
            .Property(u => u.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .UseIdentityColumn()
            .IsRequired();

        builder
            .Property(u => u.Address)
            .HasColumnName("Adress")
            .HasColumnType("varchar(40)")
            .IsRequired();

        builder.Property(u => u.Number).HasColumnName("Number").HasColumnType("int").IsRequired();

        builder
            .Property(u => u.District)
            .HasColumnName("District")
            .HasColumnType("varchar(30)")
            .IsRequired();

        builder
            .Property(u => u.City)
            .HasColumnName("City")
            .HasColumnType("varchar(20)")
            .IsRequired();

        builder
            .Property(u => u.State)
            .HasColumnName("State")
            .HasColumnType("varchar(20)")
            .IsRequired();

        builder.Property(u => u.ZipCode).HasColumnName("ZipCode").HasColumnType("int").IsRequired();
    }
}
