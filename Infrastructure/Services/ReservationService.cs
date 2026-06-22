using System.Net.WebSockets;
using Domain.DTOs;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Infrastructure.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IPropertyRepository _propertyRepository;
    private readonly IRoomTypeRepository _roomTypeRepository;
    private readonly IRoomTypeInventoryRepository _roomTypeInventoryRepository;

    public ReservationService(
        IReservationRepository reservationRepository,
        IPropertyRepository propertyRepository,
        IRoomTypeRepository roomTypeRepository,
        IRoomTypeInventoryRepository roomTypeInventoryRepository )
    {
        _reservationRepository = reservationRepository;
        _propertyRepository = propertyRepository;
        _roomTypeRepository = roomTypeRepository;
        _roomTypeInventoryRepository = roomTypeInventoryRepository;
    }

    public async Task<IEnumerable<SearchResult>> GetAvaliableVariants( VariantsFilter filter )
    {
        if ( filter.arrivalDate >= filter.departureDate )
        {
            throw new ArgumentException( "Дата заезда должна быть раньше даты выезда" );
        }

        if ( filter.guestCount <= 0 )
        {
            throw new ArgumentException( "Количество гостей должно быть больше нуля" );
        }

        IEnumerable<Property> allProperties = await _propertyRepository.GetAllAsync();

        IEnumerable<Property> propertiesInCountry = allProperties.Where( p => p.Country.Equals( filter.Country, StringComparison.OrdinalIgnoreCase ) );

        IEnumerable<Property> propertiesInCity = propertiesInCountry.Where( p => p.City.Equals( filter.city, StringComparison.OrdinalIgnoreCase ) );


        List<SearchResult> results = new();

        foreach ( Property property in propertiesInCity )
        {
            IEnumerable<RoomType> roomTypes = await _roomTypeRepository.GetAllByPropertyIdAsync( property.Id );

            IEnumerable<RoomType> filtredRoomTypes =
                roomTypes.Where( rt =>
                    rt.MinPersonCount <= filter.guestCount &&
                    rt.MaxPersonCount >= filter.guestCount )
                .Where( rt => rt.DailyPrice <= filter.maxPrice );

            foreach ( RoomType roomType in filtredRoomTypes )
            {
                bool isAvaliable = await IsRoomIsAvaliableAsync(
                    roomType.Id,
                    filter.arrivalDate,
                    filter.departureDate );

                if ( isAvaliable )
                {
                    results.Add( new SearchResult(
                        property,
                        roomType ) );
                }
            }
        }

        return results;
    }

    public async Task<IEnumerable<Reservation>> GetFilteredReservationAsync( ReservationFilter filter )
    {
        return await _reservationRepository.GetFilteredAsync( filter );
    }

    public async Task<Reservation?> GetReservationByIdAsync( int id )
    {
        return await _reservationRepository.GetByIdAsync( id );
    }

    public async Task<Reservation> AddReservationAsync( Reservation reservation, int guestCount )
    {
        bool propertyExixts = await _propertyRepository.ExistsAsync( reservation.PropertyId );
        if ( !propertyExixts )
        {
            throw new KeyNotFoundException( $"Отель с ID {reservation.PropertyId} не найден" );
        }

        var roomType = await _roomTypeRepository.GetByIdAsync( reservation.RoomTypeId );
        if ( roomType == null )
        {
            throw new KeyNotFoundException( $"Комната с ID {reservation.RoomTypeId} не найдена" );
        }

        if ( roomType.PropertyId != reservation.PropertyId )
        {
            throw new ArgumentException( $"Комната с ID {reservation.RoomTypeId} не принадлежит отелю с ID {reservation.PropertyId}" );
        }

        if ( reservation.ArrivalDate >= reservation.DepartureDate )
        {
            throw new ArgumentException( "Дата заезда должна быть раньше даты выезда" );
        }

        if ( guestCount < roomType.MinPersonCount || guestCount > roomType.MaxPersonCount )
        {
            throw new ArgumentException( $"Количество гостей должно быть в пределах от {roomType.MinPersonCount} до {roomType.MaxPersonCount}" );
        }

        bool isAvaliable = await IsRoomIsAvaliableAsync(
            reservation.RoomTypeId,
            reservation.ArrivalDate,
            reservation.DepartureDate );

        if ( !isAvaliable )
        {
            throw new InvalidOperationException( "Выбранная комната недоступна для данного периода" );
        }

        int nightCount = ( reservation.DepartureDate - reservation.ArrivalDate ).Days;
        int total = nightCount * roomType.DailyPrice;

        Reservation newReservation = new(
            0,
            reservation.PropertyId,
            reservation.RoomTypeId,
            reservation.ArrivalDate,
            reservation.DepartureDate,
            reservation.GuestName,
            reservation.GuestPhoneNumber,
            total,
            roomType.Currency
         );

        return await _reservationRepository.AddAsync( newReservation );
    }

    public async Task UpdateReservationAsync( Reservation reservation )
    {
        Reservation? existing = await _reservationRepository.GetByIdAsync( reservation.Id );
        if ( existing == null )
        {
            throw new KeyNotFoundException( $"Бронирование с ID {reservation.Id} не найдено" );
        }

        if ( reservation.ArrivalDate >= reservation.DepartureDate )
        {
            throw new ArgumentException( "Дата заезда должна быть раньше даты выезда" );
        }

        if ( reservation.ArrivalDate != existing.ArrivalDate ||
             reservation.DepartureDate != existing.DepartureDate )
        {
            bool isAvaliable = await IsRoomIsAvaliableAsync(
                reservation.RoomTypeId,
                reservation.ArrivalDate,
                reservation.DepartureDate,
                reservation.Id );

            if ( !isAvaliable )
            {
                throw new InvalidOperationException( "Выбранная комната недоступна для данного периода" );
            }
        }

        existing.Update(
            reservation.ArrivalDate,
            reservation.DepartureDate,
            reservation.GuestName,
            reservation.GuestPhoneNumber
        );

        await _reservationRepository.UpdateAsync( existing );
    }

    public async Task DeleteReservationAsync( int id )
    {
        Reservation? existing = await _reservationRepository.GetByIdAsync( id );
        if ( existing == null )
        {
            throw new KeyNotFoundException( $"Бронирование с ID {id} не найдено" );
        }

        await _reservationRepository.DeleteAsync( id );
    }

    private async Task<bool> IsRoomIsAvaliableAsync(
        int roomTypeId,
        DateTime arrivalDate,
        DateTime departureDate,
        int? excludeReservationId = null )
    {
        RoomTypeInventory? inventory = await _roomTypeInventoryRepository.GetByRoomTypeIdAsync( roomTypeId );
        if ( inventory == null )
        {
            return false;
        }

        ReservationFilter filter = new( 
            RoomTypeId: roomTypeId,
            ArrivalDate: arrivalDate,
            DepartureDate: departureDate
        );
         
        IEnumerable<Reservation> reservations = await _reservationRepository.GetFilteredAsync( filter );

        if (excludeReservationId.HasValue)
        {
            reservations = reservations.Where(r => r.Id != excludeReservationId.Value);
        }

        return reservations.Count() < inventory.RoomCount;
    }
}
