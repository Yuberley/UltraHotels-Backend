using HotelHub.Domain.Guests;
using HotelHub.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace HotelHub.Infrastructure.Repositories;

public class GuestRepository : IGuestRepository
{
    
    private readonly ApplicationDbContext _dbContext;
    
    public GuestRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    
    public Task<Guest?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Guests.FirstOrDefaultAsync(g => g.Id == id, cancellationToken);
    }
    
    public void AddList(List<Guest> guests)
    {
        _dbContext.Guests.AddRange(guests);
    }
    
    public Task<List<Guest>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.Guests.ToListAsync(cancellationToken);
    }
    
}