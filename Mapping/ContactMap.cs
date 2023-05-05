

using Microsoft.EntityFrameworkCore;
using kazariobranco_backend.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace kazariobranco_backend.Mapping;

public class ContactMap : IEntityTypeConfiguration<ContactModel>
{
    public void Configure(EntityTypeBuilder<ContactModel> builder)
    {
        builder.ToTable("contacts");
        builder.Property(p => p.id)
            .HasColumnName("id")
            .HasColumnType("char(11)")
            .IsRequired();
        builder.Property(p => p.name)
            .HasColumnName("name")
            .HasColumnType("varchar(40)");
        builder.Property(p => p.email)
            .HasColumnName("email")
            .HasColumnType("varchar(40)")
            .IsRequired();
        builder.Property(p => p.reason)
            .HasColumnName("reason")
            .HasColumnType("char(11)")
            .IsRequired();
        builder.Property(p => p.description)
            .HasColumnName("description")
            .HasColumnType("mediumtext")
            .IsRequired();
        builder.Property(p => p.created_at)
            .HasColumnName("created_at")
            .HasColumnType("date")
            .IsRequired();
        builder.Property(p => p.ended)
            .HasColumnType("bit")
            .HasDefaultValue(0);

        builder.HasKey(p => p.id).HasName("pk_contact_id");
    }
}
