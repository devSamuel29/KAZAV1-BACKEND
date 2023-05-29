using Microsoft.EntityFrameworkCore;
using kazariobranco_backend.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace kazariobranco_backend.Mapping;

public class ContactMap : IEntityTypeConfiguration<ContactModel>
{
    public void Configure(EntityTypeBuilder<ContactModel> builder)
    {
        builder.ToTable("Contacts");

        builder.HasKey(p => p.Id).HasName("pk_contact_id");
        builder.Property(p => p.Id).HasColumnName("Id").HasColumnType("int").UseIdentityColumn();

        builder
            .Property(p => p.Name)
            .HasColumnName("Name")
            .HasColumnType("varchar(40)")
            .IsRequired();

        builder
            .Property(p => p.Email)
            .HasColumnName("Email")
            .HasColumnType("varchar(40)")
            .IsRequired();

        builder
            .Property(p => p.Phone)
            .HasColumnName("Phone")
            .HasColumnType("varchar(11)")
            .IsRequired();

        builder
            .Property(p => p.Reason)
            .HasColumnName("Reason")
            .HasColumnType("varchar(11)")
            .IsRequired();

        builder
            .Property(p => p.Description)
            .HasColumnName("Description")
            .HasColumnType("text")
            .IsRequired();

        builder
            .Property(p => p.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("date")
            .IsRequired();

        builder
            .Property(p => p.EndedAt)
            .HasColumnName("EndedAt")
            .HasColumnType("date")
            .IsRequired();
    }
}
