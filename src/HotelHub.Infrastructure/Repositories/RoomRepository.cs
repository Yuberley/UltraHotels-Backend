using HotelHub.Domain.Hotels;
using HotelHub.Domain.Rooms;
using HotelHub.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace HotelHub.Infrastructure.Repositories;

internal sealed class RoomRepository : IRoomRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public RoomRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    
    public async Task<Room?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Rooms.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }
    
    public Task<List<Room>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.Rooms.ToListAsync(cancellationToken);
    }
    
    
    public void Add(Room room)
    {
        _dbContext.Rooms.Add(room);
    }
    
    public void Update(Room room)
    {
        var existingRoom = _dbContext.Rooms.Find(room.Id);
        
        if (existingRoom != null)
        {
            _dbContext.Entry(existingRoom).State = EntityState.Detached;
        }
        
        _dbContext.Rooms.Update(room);
    }
    
    public void Delete(Room room)
    {
        _dbContext.Rooms.Remove(room);
    }
}