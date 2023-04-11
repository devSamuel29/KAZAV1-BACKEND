using Microsoft.EntityFrameworkCore;

namespace kazariobranco_backend.Models.Mapping;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(p => p.id);
        builder.Property(p => p.id).HasColumnName("id").HasColumnType("int").IsRequired();

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
            .HasColumnType("varbinary(32)");

        builder.HasIndex(p => p.phone).IsUnique();
        builder.Property(p => p.phone)
            .HasColumnName("phone")
            .HasColumnType("varbinary(32)");

        builder.HasIndex(p => p.email).IsUnique();
        builder.Property(p => p.email)
            .HasColumnName("email")
            .HasColumnType("varbinary(32)")
            .IsRequired();

        builder.Property(p => p.created_at)
            .HasColumnName("created_at")
            .HasColumnType("date")
            .IsRequired();

        builder.Property(p => p.birthday)
            .HasColumnName("birthday")
            .HasColumnType("date")
            .IsRequired();
            
        builder.Property(p => p.updated_at)
            .HasColumnName("updated_at")
            .HasColumnType("date")
            .IsRequired();
    }
}
