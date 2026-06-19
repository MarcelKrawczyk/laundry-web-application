using LaundryWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace LaundryWebApplication.Data;

public class LaundryContext : DbContext
{
    public LaundryContext(DbContextOptions<LaundryContext> options) : base(options)
    {
    }
    
    public DbSet<Customer> Customers { get; set; }
    public DbSet<WashProgram> WashPrograms { get; set; }
    public DbSet<WashingMachine> WashingMachines { get; set; }
    public DbSet<PurchaseHistory> PurchaseHistories { get; set; }
    public DbSet<AvailableProgram> AvailablePrograms { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AvailableProgram>()
            .Property(e => e.Price).HasPrecision(10, 2);

        modelBuilder.Entity<PurchaseHistory>()
            .HasKey(e => new { e.AvailableProgramId, e.CustomerId });
        
        modelBuilder.Entity<WashingMachine>()
            .Property(e => e.MaxWeight).HasPrecision(10, 2);
    }
}