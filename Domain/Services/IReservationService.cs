using Domain.DTOs;
using Domain.Entities;

namespace Domain.Services;

public interface IReservationService
{
    Task<IEnumerable<SearchResult>> GetAvaliableVariants( VariantsFilter filter );
    Task<IEnumerable<Reservation>> GetFilteredReservationAsync( ReservationFilter filter );
    Task<Reservation?> GetReservationByIdAsync( int id );
    Task<Reservation> AddReservationAsync( Reservation reservation, int guestCount );
    Task UpdateReservationAsync( Reservation reservation );
    Task DeleteReservationAsync( int id );
}
