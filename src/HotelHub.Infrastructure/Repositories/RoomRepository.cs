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
    
    
    
    public Task<List<Room>> SearchAsync(DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken = default)
    {
        // Filtrar las habitaciones activas
        var activeRooms = _dbContext.Rooms.Where(r => r.IsActive.Value != false);
        
        // Obtener las reservas que se solapan con el rango de fechas
        var overlappingBookings = _dbContext.Bookings
            .Where(b => b.Duration.Start <= endDate && b.Duration.End >= startDate)
            .Select(b => b.RoomId);
        
        // Filtrar las habitaciones que no tienen reservas que se superpongan con el rango de fechas
        var availableRooms = activeRooms.Where(r => !overlappingBookings.Contains(r.Id));
        
        // Ejecutar la consulta y devolver los resultados
        return availableRooms.ToListAsync(cancellationToken);
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