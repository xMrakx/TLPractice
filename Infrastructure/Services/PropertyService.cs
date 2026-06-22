using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Infrastructure.Services;

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepository;

    public PropertyService( IPropertyRepository repository )
    {
        _propertyRepository = repository;
    }

    public async Task<IEnumerable<Property>> GetAllPropertiesAsync()
    {
        return await _propertyRepository.GetAllAsync();
    }

    public async Task<Property?> GetPropertyByIdAsync( int id )
    {
        return await _propertyRepository.GetByIdAsync( id );
    }

    public async Task<Property> AddPropertyAsync( Property property )
    {
        Property newProperty = await _propertyRepository.AddAsync( property );

        return newProperty;
    }

    public async Task UpdatePropertyAsync( Property property )
    {
        Property? existingProperty = await _propertyRepository.GetByIdAsync( property.Id );
        if ( existingProperty == null )
        {
            throw new KeyNotFoundException( $"Отель с ID {property.Id} не существует" );
        }

        await _propertyRepository.UpdateAsync( property );
    }

    public async Task DeletePropertyAsync( int id )
    {
        Property? existingProperty = await _propertyRepository.GetByIdAsync( id );
        if ( existingProperty == null )
        {
            throw new KeyNotFoundException( $"Отель с ID {id} не существует" );
        }

        await _propertyRepository.DeleteAsync( id );
    }
}
