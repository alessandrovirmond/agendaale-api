using AgendaAle.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgendaAle.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Appointment> Appointments { get; set; } 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.HasIndex(u => u.Email).IsUnique();
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Title).IsRequired().HasMaxLength(100);
            entity.Property(a => a.Description).HasMaxLength(500);
            entity.Property(a => a.Date).IsRequired();

            entity.HasOne(a => a.User)            
                  .WithMany(u => u.Appointments)
                  .HasForeignKey(a => a.UserId) 
                  .OnDelete(DeleteBehavior.Cascade); 
        });
    }
}