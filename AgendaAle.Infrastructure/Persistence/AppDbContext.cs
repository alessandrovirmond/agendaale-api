using AgendaAle.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgendaAle.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
            
            entity.HasIndex(e => e.Email).IsUnique(); 
            
            entity.Property(e => e.ExternalAuthId).IsRequired();
        });
    }
}