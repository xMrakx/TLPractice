using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Infrastructure.Services;

public class RoomTypeService : IRoomTypeService
{
    private readonly IRoomTypeRepository _roomTypeRepository;
    private readonly IPropertyRepository _propertyRepository;

    public RoomTypeService( IRoomTypeRepository roomTypeRepository, IPropertyRepository propertyRepository )
    {
        _roomTypeRepository = roomTypeRepository;
        _propertyRepository = propertyRepository;
    }

    public async Task<IEnumerable<RoomType>> GetAllRoomTypesByPropertyIdAsync( int propertyId )
    {
        bool propertyExists = await _propertyRepository.ExistsAsync( propertyId );
        if ( !propertyExists )
        {
            throw new KeyNotFoundException( $"Отель с ID {propertyId} не существует" );
        }

        return await _roomTypeRepository.GetAllByPropertyIdAsync( propertyId );
    }

    public async Task<RoomType?> GetRoomTypeByIdAsync( int roomId )
    {
        return await _roomTypeRepository.GetByIdAsync( roomId );
    }

    public async Task<RoomType> AddRoomTypeAsync( int propertyId, RoomType roomType )
    {
        bool propertyExists = await _propertyRepository.ExistsAsync( propertyId );
        if ( !propertyExists )
        {
            throw new KeyNotFoundException( $"Отель с ID {propertyId} не существует" );
        }

        RoomType newRoomType = new(
            0,
            propertyId,
            roomType.Name,
            roomType.DailyPrice,
            roomType.Currency,
            roomType.MinPersonCount,
            roomType.MaxPersonCount,
            roomType.Services ?? string.Empty,
            roomType.Amenities ?? string.Empty
        );
        return await _roomTypeRepository.AddAsync( newRoomType );
    }

    public async Task UpdateRoomTypeAsync( RoomType roomType )
    {
        RoomType? existingRoomType = await _roomTypeRepository.GetByIdAsync( roomType.Id );
        if ( existingRoomType == null )
        {
            throw new KeyNotFoundException( $"Номера с ID {roomType.Id} не существует" );
        }

        await _roomTypeRepository.UpdateAsync( roomType );
    }

    public async Task DeleteRoomTypeAsync( int id )
    {
        var existingRoomType = await _roomTypeRepository.GetByIdAsync( id );
        if ( existingRoomType == null )
        {
            throw new KeyNotFoundException( $"Номера с ID {id} не существует" );
        }

        await _roomTypeRepository.DeleteAsync( id );
    }
}
