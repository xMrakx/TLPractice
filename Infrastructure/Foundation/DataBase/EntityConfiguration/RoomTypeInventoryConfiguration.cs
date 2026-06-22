using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.DataBase.EntityConfiguration;

public class RoomTypeInventoryConfiguration : IEntityTypeConfiguration<RoomTypeInventory>
{
    public void Configure( EntityTypeBuilder<RoomTypeInventory> builder )
    {
        builder.ToTable( nameof( RoomTypeInventory ) );

        builder.HasKey( r => r.Id );

        builder.HasOne<RoomType>()
            .WithOne()
            .HasForeignKey<RoomTypeInventory>( r => r.RoomTypeId )
            .OnDelete( DeleteBehavior.Restrict );

        builder.Property( r => r.RoomCount )
            .IsRequired();
    }
}
