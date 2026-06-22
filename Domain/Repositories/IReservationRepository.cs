using Domain.DTOs;
using Domain.Entities;

namespace Domain.Repositories;

public interface IReservationRepository
{
    Task<IEnumerable<Reservation>> GetFilteredAsync(ReservationFilter filter);
    Task<Reservation?> GetByIdAsync( int id );
    Task<Reservation> AddAsync( Reservation reservation );
    Task UpdateAsync( Reservation reservation );
    Task DeleteAsync( int id );
}
