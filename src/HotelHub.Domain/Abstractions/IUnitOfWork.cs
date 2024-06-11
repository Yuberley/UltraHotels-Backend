namespace HotelHub.Domain.Abstractions;

/// <summary>
/// Unit of work es un patrón de diseño que se utiliza para garantizar la integridad de los
/// datos cuando se realizan múltiples operaciones de base de datos en una sola transacción.
/// </summary>
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}