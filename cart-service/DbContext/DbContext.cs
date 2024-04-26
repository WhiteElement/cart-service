using cart_service.Model;
using Microsoft.EntityFrameworkCore;

namespace cart_service.DbContext;

public class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<ShoppingItem> ShoppingItems { get; set; }
    public DbSet<ShoppingList> ShoppingLists { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("MyDb");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ShoppingItem>()
            .HasOne(c => c.ShoppingList)
            .WithMany(u => u.Items)
            .HasForeignKey(c => c.ShoppingListId);
    }
}