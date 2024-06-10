using Asp.Versioning;
using HotelHub.Application.Abstractions.Clock;
using HotelHub.Application.Abstractions.Data;
using HotelHub.Domain.Abstractions;
using HotelHub.Domain.Hotels;
using Microsoft.EntityFrameworkCore;
using HotelHub.Infrastructure.Clock;
using HotelHub.Infrastructure.Database;
using HotelHub.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace HotelHub.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        
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
        
        // Register repositories here using the AddScoped method
        services.AddScoped<IHotelRepository, HotelRepository>();

        
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<IDbConnectionFactory>(_ =>
            new DbConnectionFactory(connectionString));

        // SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
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