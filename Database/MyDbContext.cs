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

    public DbSet<UserModel> Users { get; set; }

    public DbSet<AddressModel> Addresses { get; set; }

    public DbSet<CartModel> Carts { get; set; }

    public DbSet<ProductModel> Products { get; set; }
    
    public DbSet<ContactModel> Contacts { get; set; }
}
