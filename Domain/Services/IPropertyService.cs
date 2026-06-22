using Domain.Entities;

namespace Domain.Services;

public interface IPropertyService
{
    Task<IEnumerable<Property>> GetAllPropertiesAsync();
    Task<Property?> GetPropertyByIdAsync( int id );
    Task<Property> AddPropertyAsync( Property property );
    Task UpdatePropertyAsync( Property property );
    Task DeletePropertyAsync( int id );
}
