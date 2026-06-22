using Domain.DTOs;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Foundation.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EfReservationRepository : IReservationRepository
{
    private readonly WebApiDbContext _dbContext;

    public EfReservationRepository( WebApiDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Reservation>> GetFilteredAsync( ReservationFilter filter )
    {
        IQueryable<Reservation> query = _dbContext.Reservations.AsQueryable();
        ApplyFilter( query, filter );

        return await query.ToListAsync();
    }

    public async Task<Reservation?> GetByIdAsync( int id )
    {
        return await _dbContext.Reservations.FirstOrDefaultAsync( r => r.Id == id );
    }

    public async Task<Reservation> AddAsync( Reservation reservation )
    {
        _dbContext.Reservations.Add( reservation );
        await _dbContext.SaveChangesAsync();

        return reservation;
    }

    public async Task UpdateAsync( Reservation reservation )
    {
        Reservation? existing = await _dbContext.Reservations.FirstOrDefaultAsync( r => r.Id == reservation.Id );
        if ( existing != null )
        {
            existing.Update(
                reservation.ArrivalDate,
                reservation.DepartureDate,
                reservation.GuestName,
                reservation.GuestPhoneNumber
            );

            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync( int id )
    {
        Reservation? reservation = await _dbContext.Reservations.FirstOrDefaultAsync( r => r.Id == id );
        if ( reservation != null )
        {
            _dbContext.Reservations.Remove( reservation );

            await _dbContext.SaveChangesAsync();
        }
    }

    private IQueryable<Reservation> ApplyFilter(
        IQueryable<Reservation> query,
        ReservationFilter filter )
    {
        if ( filter.PropertyId.HasValue )
        {
            query = query.Where( r => r.PropertyId == filter.PropertyId.Value );
        }

        if ( filter.RoomTypeId.HasValue )
        {
            query = query.Where( r => r.RoomTypeId == filter.RoomTypeId.Value );
        }

        if ( filter.ArrivalDate.HasValue )
        {
            query = query.Where( r => r.ArrivalDate >= filter.ArrivalDate.Value );
        }

        if ( filter.DepartureDate.HasValue )
        {
            query = query.Where( r => r.DepartureDate <= filter.DepartureDate.Value );
        }

        if ( !String.IsNullOrEmpty( filter.GuestName ) )
        {
            query = query.Where( r => r.GuestName.Contains( filter.GuestName ) );
        }

        if ( !String.IsNullOrEmpty( filter.Country ) )
        {
            query = query.Join(
                    _dbContext.Properties,
                    r => r.PropertyId,
                    p => p.Id,
                    ( r, p ) => new { Reservation = r, Property = p }
                )
                .Where( rp => rp.Property.Country.Contains( filter.Country ) )
                .Select( rp => rp.Reservation );
        }

        if ( !String.IsNullOrEmpty( filter.City ) )
        {
            query = query.Join(
                    _dbContext.Properties,
                    r => r.PropertyId,
                    p => p.Id,
                    ( r, p ) => new { Reservation = r, Property = p }
                )
                .Where( rp => rp.Property.City.Contains( filter.City ) )
                .Select( rp => rp.Reservation );
        }

        return query;
    }
}
