using System.Data;
using Bogus;
using Dapper;
using HotelHub.Application.Abstractions.Data;
using HotelHub.Domain.Rooms;


namespace HotelHub.WebAPI.Extrensions;

internal static class SeedDataExtentions
{
    public static void SeedData(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        
        IDbConnectionFactory connectionFactory = scope.ServiceProvider.GetRequiredService<IDbConnectionFactory>();
        using IDbConnection connection = connectionFactory.CreateConnection();
        
        var faker = new Faker();
        Random random = new Random();
        
        List<object> hotels = new();
        List<object> rooms = new();
        
        
        var roomTypesArray = RoomType.All.ToArray();
        
        for (int i = 0; i < 20; i++)
        {
            var idHotel = Guid.NewGuid();
            
            hotels.Add(new
            {
                Id = idHotel,
                Name = faker.Company.CompanyName(),
                Description = faker.Lorem.Sentence(),
                Country = faker.Address.Country(),
                State = faker.Address.State(),
                ZipCode = faker.Address.ZipCode(),
                City = faker.Address.City(),
                Street = faker.Address.StreetAddress(),
                IsActive = true,
                CreatedAtOnUtc = DateTime.UtcNow
            });
            
            for (int j = 0; j < 10; j++)
            {
                var idRoom = Guid.NewGuid();
                
                var randomType = roomTypesArray[ random.Next( roomTypesArray.Length ) ];
                
                rooms.Add(new
                {
                    Id = idRoom,
                    RoomNumber = faker.Random.Number(1, 100),
                    Type = randomType.Value,
                    BaseCostAmount = faker.Random.Decimal(50, 1000),
                    BaseCostCurrency = "USD",
                    Taxes = faker.Random.Decimal(5, 20),
                    IsActive = true,
                    CreatedAtOnUtc = DateTime.UtcNow,
                    HotelId = idHotel
                });
            }
            
            
        }
        
        const string sqlHotels = """
            INSERT INTO public.hotels
            (id, name, description, address_country, address_state, address_zip_code, address_city, address_street, is_active, created_at_on_utc)
            VALUES(@Id, @Name, @Description, @Country, @State, @ZipCode, @City, @Street, @IsActive, @CreatedAtOnUtc);
            """;
        
        const string sqlRooms = """
            INSERT INTO public.rooms
            (id, room_number, type, base_cost_amount, base_cost_currency, taxes, is_active, created_at_on_utc, hotel_id)
            VALUES(@Id, @RoomNumber, @Type, @BaseCostAmount, @BaseCostCurrency, @Taxes, @IsActive, @CreatedAtOnUtc, @HotelId);
            """;
        
        connection.Execute(sqlHotels, hotels);
        connection.Execute(sqlRooms, rooms);
    }
}