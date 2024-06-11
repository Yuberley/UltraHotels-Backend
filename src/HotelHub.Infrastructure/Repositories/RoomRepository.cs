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
    
    public Task<List<Room?>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.Rooms.ToListAsync(cancellationToken);
    }
    
    public async Task<List<Room>> GetByHotelIdAsync(Guid hotelId, CancellationToken cancellationToken)
    {
        return await _dbContext.Rooms.Where(r => r.HotelId == hotelId).ToListAsync(cancellationToken);
    }
    
    public void Add(Room room)
    {
        _dbContext.Rooms.Add(room);
    }
    
    public void Update(Room room)
    {
        _dbContext.Rooms.Update(room);
    }
    
    public void Delete(Room room)
    {
        _dbContext.Rooms.Remove(room);
    }
}