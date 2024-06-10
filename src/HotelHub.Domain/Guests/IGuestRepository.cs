namespace HotelHub.Domain.Guests;

public interface IGuestRepository
{
    Task<Guest?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Guest>> GetAllAsync(CancellationToken cancellationToken = default);
    void Insert(Guest guest);
}