using HotelHub.Domain.Hotels;
using HotelHub.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace HotelHub.Infrastructure.Repositories;

internal sealed class HotelRepository : IHotelRepository
{
    
    private readonly ApplicationDbContext _dbContext;
    
    public HotelRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    
    public Task<Hotel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Hotels.FirstOrDefaultAsync(h => h.Id == id, cancellationToken);
    }
    
    public Task<List<Hotel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.Hotels.ToListAsync(cancellationToken);
    }
    
    public void Add(Hotel hotel)
    {
        _dbContext.Hotels.Add(hotel);
    }
    
    public void Update(Hotel hotel)
    {
        _dbContext.Hotels.Update(hotel);
    }
    
    public void Delete(Hotel hotel)
    {
        _dbContext.Hotels.Remove(hotel);
    }
}