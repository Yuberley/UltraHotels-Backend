namespace HotelHub.Domain.Guests;

public interface IGuestRepository
{
    Task<Guest?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void AddList(List<Guest> guests);
    Task<List<Guest>> GetAllAsync(CancellationToken cancellationToken = default);
}