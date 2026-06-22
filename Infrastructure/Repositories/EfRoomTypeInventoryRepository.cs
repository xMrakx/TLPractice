using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Foundation.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EfRoomTypeInventoryRepository : IRoomTypeInventoryRepository
{
    private readonly WebApiDbContext _dbContext;

    public EfRoomTypeInventoryRepository( WebApiDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public async Task<RoomTypeInventory?> GetByRoomTypeIdAsync( int roomTypeId )
    {
        return await _dbContext.RoomTypeInventories.FirstOrDefaultAsync( r => r.RoomTypeId == roomTypeId );
    }

    public async Task AddAsync( RoomTypeInventory inventory )
    {
        await _dbContext.RoomTypeInventories.AddAsync( inventory );

        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync( RoomTypeInventory inventory )
    {
        RoomTypeInventory? existing = await _dbContext.RoomTypeInventories.FirstOrDefaultAsync( r => r.Id == inventory.Id );
        if ( existing != null )
        {
            _dbContext.Entry( existing ).CurrentValues.SetValues( inventory );

            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync( int id )
    {
        RoomTypeInventory? inventory = await _dbContext.RoomTypeInventories.FirstOrDefaultAsync( r => r.Id == id );
        if ( inventory != null )
        {
            _dbContext.RoomTypeInventories.Remove( inventory );

            await _dbContext.SaveChangesAsync();
        }
}
}
