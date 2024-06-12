using Asp.Versioning;
using HotelHub.Application.Abstractions.Authentication;
using HotelHub.Application.Abstractions.Clock;
using HotelHub.Application.Abstractions.Data;
using HotelHub.Application.Abstractions.Email;
using HotelHub.Domain.Abstractions;
using HotelHub.Domain.Bookings;
using HotelHub.Domain.Hotels;
using HotelHub.Domain.Rooms;
using HotelHub.Domain.Users;
using HotelHub.Infrastructure.Authentication;
using Microsoft.EntityFrameworkCore;
using HotelHub.Infrastructure.Clock;
using HotelHub.Infrastructure.Database;
using HotelHub.Infrastructure.Email;
using HotelHub.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace HotelHub.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        
        services.AddTransient<IEmailService, EmailService>();
        
        services.AddTransient<IJwtProvider, JwtProvider>();
        
        services.AddTransient<IHashingService, HashingService>();
        
        AddPersistence(services, configuration);
        
        // AddAuthentication(services, configuration);
        
        AddApiVersioning(services);
        
        return services;
    }
    
     private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("PostgresConnection") ??
                                  throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention());
        
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<IDbConnectionFactory>(_ =>
            new DbConnectionFactory(connectionString));

        // Register repositories here using the AddScoped method
        services.AddScoped<IHotelRepository, HotelRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }

    private static void AddApiVersioning(IServiceCollection services)
    {
        services
            .AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1);
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddMvc()
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });
    }
}