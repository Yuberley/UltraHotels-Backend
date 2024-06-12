namespace HotelHub.Domain.Rooms;

public interface IRoomRepository
{
    Task<Room?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Room>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<Room>> SearchAsync(string sqlQuery, CancellationToken cancellationToken = default);
    void Add(Room room);
    void Update(Room room);
    void Delete(Room room);
}