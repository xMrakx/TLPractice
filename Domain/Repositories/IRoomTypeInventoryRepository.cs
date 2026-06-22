using Domain.Entities;

namespace Domain.Repositories;

public interface IRoomTypeInventoryRepository
{
    Task<RoomTypeInventory?> GetByRoomTypeIdAsync( int roomTypeId );
    Task AddAsync( RoomTypeInventory inventory );
    Task UpdateAsync( RoomTypeInventory inventory );
    Task DeleteAsync( int id );
}
