namespace HotelHub.Application.Hotels;

public sealed class HotelResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public AddressResponse Address { get; set; }
    public bool IsActive { get; init; }
}

public sealed class AddressResponse
{
    public string Country { get; init; }
    
    public string State { get; init; }
    
    public string ZipCode { get; init; }
    
    public string City { get; init; }
    
    public string Street { get; init; }
}