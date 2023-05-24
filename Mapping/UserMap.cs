using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using kazariobranco_backend.Models;
namespace kazariobranco_backend.Mapping;

public class UserMap : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(p => p.Id).HasName("pk_user_id");
        builder.Property(p => p.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .UseIdentityColumn()
            .IsRequired();

        builder.Property(p => p.Firstname)
            .HasColumnName("firstname")
            .HasColumnType("varchar(20)")
            .IsRequired();

        builder.Property(p => p.Lastname)
            .HasColumnName("lastname")
            .HasColumnType("varchar(20)")
            .IsRequired();

        builder.HasIndex(p => p.Cpf).IsUnique();
        builder.Property(p => p.Cpf)
            .HasColumnName("cpf")
            .HasColumnType("varchar(84)")
            .IsRequired();

        builder.HasIndex(p => p.Phone).IsUnique();
        builder.Property(p => p.Phone)
            .HasColumnName("phone")
            .HasColumnType("varchar(32)")
            .IsRequired();

        builder.HasIndex(p => p.Email).IsUnique();
        builder.Property(p => p.Email)
            .HasColumnName("email")
            .HasColumnType("varchar(40)")
            .IsRequired();

        builder.Property(p => p.Password)
            .HasColumnName("password")
            .HasColumnType("varchar(85)")
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("datetime2")
            .IsRequired();

        builder.Property(p => p.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("datetime2")
            .IsRequired();
    }
}
