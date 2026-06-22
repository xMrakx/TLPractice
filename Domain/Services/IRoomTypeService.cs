using System.Collections;
using Domain.Entities;

namespace Domain.Services;

public interface IRoomTypeService
{
    Task<IEnumerable<RoomType>> GetAllRoomTypesByPropertyIdAsync( int propertyId );
    Task<RoomType> GetRoomTypeByIdAsync( int roomId );
    Task<RoomType> AddRoomTypeAsync(int propertyId, RoomType roomType );
    Task UpdateRoomTypeAsync( RoomType roomType );
    Task DeleteRoomTypeAsync( int id );
}
