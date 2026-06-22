using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Foundation.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EfRoomTypeRepository : IRoomTypeRepository
{
    private readonly WebApiDbContext _dbContext;
    public EfRoomTypeRepository( WebApiDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<RoomType>> GetAllByPropertyIdAsync( int propertyId )
    {
        return await _dbContext.RoomTypes.Where( rt => rt.PropertyId == propertyId ).ToListAsync();
    }

    public async Task<RoomType?> GetByIdAsync( int id )
    {
        return await _dbContext.RoomTypes.FirstOrDefaultAsync( rt => rt.Id == id );
    }

    public async Task<RoomType> AddAsync( RoomType roomType )
    {
        await _dbContext.RoomTypes.AddAsync( roomType );
        await _dbContext.SaveChangesAsync();

        return roomType;
    }

    public async Task UpdateAsync( RoomType roomType )
    {
        RoomType? existing = await _dbContext.RoomTypes.FirstOrDefaultAsync( rt => rt.Id == roomType.Id );
        if ( existing != null )
        {
            existing.Update(
                roomType.Name,
                roomType.DailyPrice,
                roomType.Currency,
                roomType.MinPersonCount,
                roomType.MaxPersonCount,
                roomType.Services,
                roomType.Amenities
            );

            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync( int id )
    {
        RoomType? roomType = await _dbContext.RoomTypes.FirstOrDefaultAsync( rt => rt.Id == id );
        if ( roomType != null )
        {
            _dbContext.RoomTypes.Remove( roomType );

            await _dbContext.SaveChangesAsync();
        }
    }

    public Task<bool> ExistsAsync( int id )
    {
        return _dbContext.RoomTypes.AnyAsync( rt => rt.Id == id );
    }
}
