using Domain.DTOs;
using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Validation;
using WebApi.DTOs.PropertyDTOs;
using WebApi.DTOs.ReservationDTOs;
using WebApi.DTOs.RoomTypeDTOs;

namespace WebApi.Controllers;

[ApiController]
[Route( "api/reservations" )]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationsController( IReservationService reservationService )
    {
        _reservationService = reservationService;
    }

    [HttpGet( "variants" )]
    public async Task<IActionResult> SearchVariants( [FromQuery] VariantsRequestDto requestDto )
    {
        VariantsFilter filter = new(
            requestDto.Country,
            requestDto.City,
            requestDto.ArrivalDate,
            requestDto.DepartureDate,
            requestDto.GuestCount,
            requestDto.MaxPrice
        );

        try
        {
            IEnumerable<SearchResult> variants = await _reservationService.GetAvaliableVariants( filter );

            IEnumerable<SearchResultDto> dtos = variants.Select( v => new SearchResultDto(
                new PropertyResponseDto(
                    v.Property.Id,
                    v.Property.Name,
                    v.Property.Country,
                    v.Property.City,
                    v.Property.Address,
                    v.Property.Latitude,
                    v.Property.Longitude
                ),
                new RoomTypeResponseDto(
                    v.RoomType.Id,
                    v.RoomType.PropertyId,
                    v.RoomType.Name,
                    v.RoomType.DailyPrice,
                    v.RoomType.Currency,
                    v.RoomType.MinPersonCount,
                    v.RoomType.MaxPersonCount,
                    v.RoomType.Services,
                    v.RoomType.Amenities
                )
             ) );

            return Ok( dtos );
        }
        catch ( ArgumentException ex )
        {
            return BadRequest( new { error = ex.Message } );
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll( [FromQuery] ReservationFilterDto filterDto )
    {
        ReservationFilter filter = new(

            filterDto.PropertyId,
            filterDto.RoomTypeId,
            filterDto.ArrivalDate,
            filterDto.DepartureDate,
            filterDto.GuestName,
            filterDto.Country,
            filterDto.City
        );

        IEnumerable<Reservation> reservations = await _reservationService.GetFilteredReservationAsync( filter );

        IEnumerable<ReservationResponseDto> dtos = reservations.Select( r => new ReservationResponseDto(
            r.Id,
            r.PropertyId,
            r.RoomTypeId,
            r.ArrivalDate,
            r.DepartureDate,
            r.GuestName,
            r.GuestPhoneNumber,
            r.Total,
            r.Currency
        ) );

        return Ok( dtos );
    }

    [HttpGet( "{id:int}" )]
    public async Task<IActionResult> GetById( int id )
    {
        Reservation? reservation = await _reservationService.GetReservationByIdAsync( id );
        if ( reservation == null )
        {
            return NotFound();
        }

        ReservationResponseDto dto = new(
            reservation.Id,
            reservation.PropertyId,
            reservation.RoomTypeId,
            reservation.ArrivalDate,
            reservation.DepartureDate,
            reservation.GuestName,
            reservation.GuestPhoneNumber,
            reservation.Total,
            reservation.Currency
        );

        return Ok( dto );
    }

    [HttpPost]
    public async Task<IActionResult> AddReservation( [FromBody] ReservationAddDto addDto )
    {
        Reservation reservation = new(
            0,
            addDto.PropertyId,
            addDto.RoomTypeId,
            addDto.ArrivalDate,
            addDto.DepartureDate,
            addDto.GuestName,
            addDto.GuestPhoneNumber,
            0,
            string.Empty
        );

        try
        {
            Reservation newReservation = await _reservationService.AddReservationAsync( reservation, addDto.GuestCount );

            ReservationResponseDto responseDto = new(
                newReservation.Id,
                newReservation.PropertyId,
                newReservation.RoomTypeId,
                newReservation.ArrivalDate,
                newReservation.DepartureDate,
                newReservation.GuestName,
                newReservation.GuestPhoneNumber,
                newReservation.Total,
                newReservation.Currency
            );

            return Ok( responseDto );
        }
        catch ( KeyNotFoundException ex )
        {
            return NotFound( new { error = ex.Message } );
        }
        catch ( ArgumentException ex )
        {
            return BadRequest( new { error = ex.Message } );
        }
        catch ( InvalidOperationException ex )
        {
            return Conflict( new { error = ex.Message } );
        }
    }

    [HttpPut( "{id:int}" )]
    public async Task<IActionResult> UpdateReservation( int id, [FromBody] ReservationUpdateDto updateDto )
    {
        Reservation reservation = new(
            id,
            0,
            0,
            updateDto.ArrivalDate,
            updateDto.DepartureDate,
            updateDto.GuestName,
            updateDto.GuestPhoneNumber,
            0,
            string.Empty
        );

        try
        {
            await _reservationService.UpdateReservationAsync( reservation );

            return NoContent();
        }
        catch ( KeyNotFoundException ex )
        {
            return NotFound( new { error = ex.Message } );
        }
        catch ( ArgumentException ex )
        {
            return BadRequest( new { error = ex.Message } );
        }
        catch ( InvalidOperationException ex )
        {
            return Conflict( new { error = ex.Message } );
        }
    }

    [HttpDelete( "{id:int}" )]
    public async Task<IActionResult> DeleteReservation( int id )
    {
        try
        {
            await _reservationService.DeleteReservationAsync( id );

            return Ok();
        }
        catch ( KeyNotFoundException ex )
        {
            return NotFound( new { error = ex.Message } );
        }
    }
}

