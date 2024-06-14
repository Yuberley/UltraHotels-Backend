namespace HotelHub.Domain.Hotels;

public interface IHotelRepository
{
    Task<Hotel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Hotel>> GetAllAsync(CancellationToken cancellationToken = default);
    void Add(Hotel hotel);
    void Update(Hotel hotel);
    void Delete(Hotel hotel);
}