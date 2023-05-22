

using Microsoft.EntityFrameworkCore;
using kazariobranco_backend.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace kazariobranco_backend.Mapping;

public class ContactMap : IEntityTypeConfiguration<ContactModel>
{
    public void Configure(EntityTypeBuilder<ContactModel> builder)
    {
        builder.ToTable("contacts");

        builder.HasKey(p => p.Id).HasName("pk_contact_id");
        builder.Property(p => p.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .UseIdentityColumn()
            .IsRequired();

        builder.Property(p => p.Name)
            .HasColumnName("name")
            .HasColumnType("varchar(40)")
            .IsRequired();

        builder.Property(p => p.Email)
            .HasColumnName("email")
            .HasColumnType("varchar(40)")
            .IsRequired();
        
        builder.Property(p => p.Phone)
            .HasColumnName("phone")
            .HasColumnType("varchar(11)")
            .IsRequired();

        builder.Property(p => p.Reason)
            .HasColumnName("reason")
            .HasColumnType("char(11)")
            .IsRequired();

        builder.Property(p => p.Description)
            .HasColumnName("description")
            .HasColumnType("text")
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("date")
            .IsRequired();

        builder.Property(p => p.Ended)
            .HasColumnType("bit");
    }
}
