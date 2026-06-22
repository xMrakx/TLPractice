using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.DataBase.EntityConfiguration;

public class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
{
    public void Configure( EntityTypeBuilder<RoomType> builder )
    {
        builder.ToTable( nameof( RoomType ) );

        builder.HasKey( r => r.Id );

        builder.HasOne<Property>()
            .WithMany( p => p.RoomTypes )
            .HasForeignKey( r => r.PropertyId )
            .OnDelete( DeleteBehavior.Restrict );

        builder.Property( r => r.Name )
            .IsRequired()
            .HasMaxLength( 50 );

        builder.Property( r => r.DailyPrice )
            .IsRequired();

        builder.Property( r => r.Currency )
            .IsRequired();

        builder.Property( r => r.MinPersonCount )
            .IsRequired();

        builder.Property( r => r.MaxPersonCount )
            .IsRequired();

        builder.Property( r => r.Services )
            .HasMaxLength( 500 );

        builder.Property( r => r.Amenities )
            .HasMaxLength( 500 );
    }
}
