using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Foundation.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EfPropertyRepository : IPropertyRepository
{
    private readonly WebApiDbContext _dbContext;

    public EfPropertyRepository( WebApiDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Property>> GetAllAsync()
    {
        return await _dbContext.Properties.ToListAsync();
    }

    public async Task<Property?> GetByIdAsync( int id )
    {
        return await _dbContext.Properties.FirstOrDefaultAsync( p => p.Id == id );
    }

    public async Task<Property> AddAsync( Property property )
    {
        await _dbContext.Properties.AddAsync( property );
        await _dbContext.SaveChangesAsync();

        return property;
    }

    public async Task UpdateAsync( Property property )
    {
        Property? existing = await _dbContext.Properties.FirstOrDefaultAsync( p => p.Id == property.Id );
        if ( existing != null )
        {
            existing.Update(
                property.Name,
                property.Country,
                property.City,
                property.Address,
                property.Latitude,
                property.Longitude
             );

            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync( int id )
    {
        Property? existing = await _dbContext.Properties.FirstOrDefaultAsync( p => p.Id == id );
        if ( existing != null )
        {
            _dbContext.Properties.Remove( existing );

            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync( int id )
    {
        return await _dbContext.Properties.AnyAsync( p => p.Id == id );
    }
}
