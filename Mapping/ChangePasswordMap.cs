using Microsoft.EntityFrameworkCore;
using kazariobranco_backend.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace kazariobranco_backend.Mapping;

public class ChangePasswordMap : IEntityTypeConfiguration<ChangePasswordModel>
{
    public void Configure(EntityTypeBuilder<ChangePasswordModel> builder)
    {
        builder.ToTable("ChangePassword");

        builder.HasKey(p => p.Id);
        builder
            .Property(p => p.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .UseIdentityColumn();

        builder
            .Property(p => p.Code)
            .HasColumnName("Code")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(p => p.Email)
            .HasColumnName("Email")
            .HasColumnType("varchar(40)")
            .IsRequired();

        builder
            .Property(p => p.IsValid)
            .HasColumnName("IsValid")
            .HasColumnType("datetime2");

        builder
            .Property(p => p.IsFinished)
            .HasColumnName("IsFinished")
            .HasColumnType("bit");
    }
}
