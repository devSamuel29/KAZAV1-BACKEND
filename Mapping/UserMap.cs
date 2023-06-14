using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using kazariobranco_backend.Models;

namespace kazariobranco_backend.Mapping;

public class UserMap : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(p => p.Id).HasName("PkUserId");
        builder.Property(p => p.Id).HasColumnName("Id").HasColumnType("int").UseIdentityColumn();

        builder
            .Property(p => p.Role)
            .HasColumnName("Role")
            .HasColumnType("varchar(5)")
            .IsRequired();

        builder
            .Property(p => p.Firstname)
            .HasColumnName("Firstname")
            .HasColumnType("varchar(20)")
            .IsRequired();

        builder
            .Property(p => p.Lastname)
            .HasColumnName("Lastname")
            .HasColumnType("varchar(20)")
            .IsRequired();

        builder.HasIndex(p => p.Cpf).IsUnique();
        builder.Property(p => p.Cpf).HasColumnName("Cpf").HasColumnType("varchar(84)").IsRequired();

        builder.HasIndex(p => p.Phone).IsUnique();
        builder
            .Property(p => p.Phone)
            .HasColumnName("Phone")
            .HasColumnType("varchar(32)")
            .IsRequired();

        builder.HasIndex(p => p.Email).IsUnique();
        builder
            .Property(p => p.Email)
            .HasColumnName("Email")
            .HasColumnType("varchar(40)")
            .IsRequired();

        builder
            .Property(p => p.Password)
            .HasColumnName("Password")
            .HasColumnType("varchar(85)")
            .IsRequired();

        builder
            .Property(p => p.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("datetime2")
            .IsRequired();

        builder
            .Property(p => p.UpdatedAt)
            .HasColumnName("UpdatedAt")
            .HasColumnType("datetime2")
            .IsRequired();
    }
}
