using kazariobranco_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace kazariobranco_backend.Database;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyDbContext).Assembly);
    }

    public DbSet<UserModel> Users { get; set; } = null!;

    public DbSet<ChangePasswordModel> ChangePassword { get; set; } = null!;

    public DbSet<AddressModel> Addresses { get; set; } = null!;

    public DbSet<CartModel> Carts { get; set; } = null!;

    public DbSet<ProductModel> Products { get; set; } = null!;

    public DbSet<ContactModel> Contacts { get; set; } = null!;
}
