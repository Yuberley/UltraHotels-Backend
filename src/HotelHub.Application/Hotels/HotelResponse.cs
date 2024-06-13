namespace HotelHub.Application.Hotels;

public sealed class HotelResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public AddressResponse Address { get; set; }
    public bool IsActive { get; init; }
}