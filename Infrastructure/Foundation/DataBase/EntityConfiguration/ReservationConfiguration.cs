using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.DataBase.EntityConfiguration;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure( EntityTypeBuilder<Reservation> builder )
    {
        builder.ToTable( nameof( Reservation ) );

        builder.HasKey( r => r.Id );

        builder.HasOne<Property>()
            .WithMany()
            .HasForeignKey( r => r.PropertyId )
            .OnDelete( DeleteBehavior.Restrict );

        builder.HasOne<RoomType>()
            .WithMany()
            .HasForeignKey( r => r.RoomTypeId )
            .OnDelete( DeleteBehavior.Restrict );

        builder.Property( r => r.ArrivalDate )
            .IsRequired();

        builder.Property( r => r.DepartureDate )
            .IsRequired();

        builder.Property( r => r.GuestName )
            .IsRequired()
            .HasMaxLength( 30 );

        builder.Property( r => r.GuestPhoneNumber )
            .IsRequired()
            .HasMaxLength( 15 );

        builder.Property( r => r.Total )
            .IsRequired();

        builder.Property( r => r.Currency )
            .IsRequired();
    }
}
