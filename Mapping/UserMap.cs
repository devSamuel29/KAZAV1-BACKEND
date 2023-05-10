using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using kazariobranco_backend.Models;
namespace kazariobranco_backend.Mapping;

public class UserMap : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        builder.ToTable("users");

        builder.HasKey(p => p.id).HasName("pk_user_id");
        builder.Property(p => p.id)
            .HasColumnName("id")
            .HasColumnType("char(32)")
            .IsRequired();

        builder.Property(p => p.firstname)
            .HasColumnName("firstname")
            .HasColumnType("varchar(20)")
            .IsRequired();

        builder.Property(p => p.lastname)
            .HasColumnName("lastname")
            .HasColumnType("varchar(20)")
            .IsRequired();

        builder.HasIndex(p => p.cpf).IsUnique();
        builder.Property(p => p.cpf)
            .HasColumnName("cpf")
            .HasColumnType("varbinary(32)")
            .IsRequired();

        builder.HasIndex(p => p.phone).IsUnique();
        builder.Property(p => p.phone)
            .HasColumnName("phone")
            .HasColumnType("varchar(32)")
            .IsRequired();

        builder.HasIndex(p => p.email).IsUnique();
        builder.Property(p => p.email)
            .HasColumnName("email")
            .HasColumnType("varchar(40)")
            .IsRequired();

        builder.Property(p => p.password)
            .HasColumnName("password")
            .HasColumnType("varchar(32)")
            .IsRequired();

        builder.Property(p => p.created_at)
            .HasColumnName("created_at")
            .HasColumnType("date")
            .IsRequired();

        builder.Property(p => p.updated_at)
            .HasColumnName("updated_at")
            .HasColumnType("date")
            .IsRequired();
    }
}
