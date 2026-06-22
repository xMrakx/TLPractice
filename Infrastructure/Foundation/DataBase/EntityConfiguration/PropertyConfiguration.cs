using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.DataBase.EntityConfiguration;

public class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure( EntityTypeBuilder<Property> builder )
    {
        builder.ToTable( nameof( Property ) );

        builder.HasKey( p => p.Id );

        builder.Property( p => p.Name )
            .IsRequired()
            .HasMaxLength( 50 );

        builder.Property( p => p.Country )
            .IsRequired()
            .HasMaxLength( 50 );

        builder.Property( p => p.City )
            .IsRequired()
            .HasMaxLength( 50 );

        builder.Property( p => p.Address )
            .IsRequired()
            .HasMaxLength( 200 );

        builder.Property( p => p.Latitude )
            .IsRequired()
            .HasPrecision( 10, 6 );

        builder.Property( p => p.Longitude )
            .IsRequired()
            .HasPrecision( 10, 6 );

        builder.HasMany( p => p.RoomTypes )
            .WithOne()
            .HasForeignKey( rt => rt.PropertyId )
            .OnDelete( DeleteBehavior.Restrict );
    }
}
