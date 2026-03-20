using Microsoft.EntityFrameworkCore;
using TowelBox.Domain.Entities;

namespace TowelBox.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Towel> Towels { get; set; }

    public DbSet<Box> Boxes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Towel>()
            .HasOne<Box>()
            .WithMany()
            .HasForeignKey(t => t.BoxId)
            .OnDelete(DeleteBehavior.Restrict);
    }
    
}