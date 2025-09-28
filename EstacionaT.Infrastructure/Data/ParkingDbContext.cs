using EstacionaT.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EstacionaT.Infrastructure.Data;

public class ParkingDbContext : DbContext
{
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Settings> Settings { get; set; }
    public DbSet<TrafficStatus> TrafficStatus { get; set; }

    public ParkingDbContext(DbContextOptions<ParkingDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Seed initial data
        modelBuilder.Entity<Settings>().HasData(new Settings { Id = 1, FreeMinutes = 15, ToleranceMinutes = 15 });
        modelBuilder.Entity<TrafficStatus>().HasData(new TrafficStatus { Id = 1, IsCongestionActive = false, ExtraToleranceMinutes = 0, UnlimitedMode = false });
    }
}