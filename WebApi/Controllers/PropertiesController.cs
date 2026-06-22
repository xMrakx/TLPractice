using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.PropertyDTOs;

namespace WebApi.Controllers;

[ApiController]
[Route( "api/properties" )]
public class PropertiesController : ControllerBase
{
    private readonly IPropertyService _propertyService;

    public PropertiesController( IPropertyService propertyService )
    {
        _propertyService = propertyService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        IEnumerable<Property> properties = await _propertyService.GetAllPropertiesAsync();
        IEnumerable<PropertyResponseDto> dtos =
            properties.Select( p => new PropertyResponseDto(
                p.Id,
                p.Name,
                p.Country,
                p.City,
                p.Address,
                p.Latitude,
                p.Longitude 
            )
        );

        return Ok( dtos );
    }

    [HttpGet( "{id:int}" )]
    public async Task<IActionResult> GetById( int id )
    {
        Property? property = await _propertyService.GetPropertyByIdAsync( id );
        if ( property == null )
        {
            return NotFound();
        }
        PropertyResponseDto dto = new(
            property.Id,
            property.Name,
            property.Country,
            property.City,
            property.Address,
            property.Latitude,
            property.Longitude
        );

        return Ok( dto );
    }

    [HttpPost]
    public async Task<IActionResult> Add( [FromBody] PropertyAddDto dto )
    {
        Property property = new(
            0,
            dto.Name,
            dto.Country,
            dto.City,
            dto.Address,
            dto.Latitude,
            dto.Longitude
        );

        var created = await _propertyService.AddPropertyAsync( property );

        PropertyResponseDto ResponseDto = new(
            created.Id,
            created.Name,
            created.Country,
            created.City,
            created.Address,
            created.Latitude,
            created.Longitude
        );

        return Ok( ResponseDto );
    }

    [HttpPut( "{id:int}" )]
    public async Task<IActionResult> Update( int id, [FromBody] PropertyUpdateDto dto )
    {
        Property property = new(
            id,
            dto.Name,
            dto.Country,
            dto.City,
            dto.Address,
            dto.Latitude,
            dto.Longitude
        );

        try
        {
            await _propertyService.UpdatePropertyAsync( property );

           return NoContent();
        }
        catch ( Exception ex )
        {
            return NotFound( new { error = ex.Message } );
        }
    }

    [HttpDelete( ( "{id:int}" ) )]
    public async Task<IActionResult> Delete( int id )
    {
        try
        {
            await _propertyService.DeletePropertyAsync( id );

            return NoContent();
        }
        catch ( Exception ex )
        {
            return NotFound( new { error = ex.Message } );
        }
    }
}
