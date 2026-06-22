using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.RoomTypeDTOs;

namespace WebApi.Controllers;

[ApiController]
[Route( "api/properties/{propertyId}/roomtypes" )]
public class RoomTypesController : ControllerBase
{
    private readonly IRoomTypeService _roomTypeService;

    public RoomTypesController( IRoomTypeService roomTypeService )
    {
        _roomTypeService = roomTypeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllByPropertyId( int propertyId )
    {
        try
        {
            IEnumerable<RoomType> roomTypes = await _roomTypeService.GetAllRoomTypesByPropertyIdAsync( propertyId );

            IEnumerable<RoomTypeResponseDto> dtos =
                roomTypes.Select( rt => new RoomTypeResponseDto(
                    rt.Id,
                    rt.PropertyId,
                    rt.Name,
                    rt.DailyPrice,
                    rt.Currency,
                    rt.MinPersonCount,
                    rt.MaxPersonCount,
                    rt.Services,
                    rt.Amenities
                )
             );

            return Ok( dtos );
        }
        catch ( KeyNotFoundException ex )
        {
            return NotFound( new { error = ex.Message } );
        }
    }

    [HttpGet( "~/api/roomtypes/{id:int}" )]
    public async Task<IActionResult> GetById( int id )
    {
        RoomType? roomType = await _roomTypeService.GetRoomTypeByIdAsync( id );
        if ( roomType == null )
        {
            return NotFound();
        }

        var dto = new RoomTypeResponseDto(
            roomType.Id,
            roomType.PropertyId,
            roomType.Name,
            roomType.DailyPrice,
            roomType.Currency,
            roomType.MinPersonCount,
            roomType.MaxPersonCount,
            roomType.Services,
            roomType.Amenities
        );

        return Ok( dto );
    }

    [HttpPost]
    public async Task<IActionResult> AddRoomType( int propertyId, [FromBody] RoomTypeAddDto dto )
    {
        RoomType roomType = new(
            0,
            propertyId,
            dto.Name,
            dto.DailyPrice,
            dto.Currency,
            dto.MinPersonCount,
            dto.MaxPersonCount,
            dto.Services,
            dto.Amenities
        );

        try
        {
            RoomType created = await _roomTypeService.AddRoomTypeAsync( propertyId, roomType );

            var responseDto = new RoomTypeResponseDto(
                created.Id,
                created.PropertyId,
                created.Name,
                created.DailyPrice,
                created.Currency,
                created.MinPersonCount,
                created.MaxPersonCount,
                created.Services,
                created.Amenities
            );

            return Ok( responseDto );
        }
        catch ( KeyNotFoundException ex )
        {
            return NotFound( new { error = ex.Message } );
        }
    }

    [HttpPut( "~/api/roomtypes/{id:int}" )]
    public async Task<IActionResult> Update( int id, [FromBody] RoomTypeUpdateDto dto )
    {
        RoomType roomType = new(
            id,
            0,
            dto.Name,
            dto.DailyPrice,
            dto.Currency,
            dto.MinPersonCount,
            dto.MaxPersonCount,
            dto.Services,
            dto.Amenities
        );

        try
        {
            await _roomTypeService.UpdateRoomTypeAsync( roomType );

            return NoContent();

        }
        catch ( KeyNotFoundException ex )
        {
            return NotFound( new { error = ex.Message } );
        }
    }

    [HttpDelete( "~/api/roomtypes/{id:int}" )]
    public async Task<IActionResult> Delete( int id )
    {
        try
        {
            await _roomTypeService.DeleteRoomTypeAsync( id );

            return NoContent();
        }
        catch ( KeyNotFoundException ex )
        {
            return NotFound( new { error = ex.Message } );
        }
    }
}
