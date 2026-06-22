using Domain.Entities;
using Infrastructure.Foundation.DataBase.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.DataBase;

public class WebApiDbContext : DbContext
{
    public WebApiDbContext( DbContextOptions<WebApiDbContext> options ) : base( options )
    {
    }

    public DbSet<Property> Properties { get; set; }
    public DbSet<RoomType> RoomTypes { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<RoomTypeInventory> RoomTypeInventories { get; set; }

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        base.OnModelCreating( modelBuilder );
        modelBuilder.ApplyConfiguration( new PropertyConfiguration() );
        modelBuilder.ApplyConfiguration( new RoomTypeConfiguration() );
        modelBuilder.ApplyConfiguration( new ReservationConfiguration() );
        modelBuilder.ApplyConfiguration( new RoomTypeInventoryConfiguration() );
    }
}
