using FluentValidation;
using HotelHub.Application.Abstractions.Behaviors;
using HotelHub.Application.Users.LogIn;
using HotelHub.Application.Users.Register;
using HotelHub.Domain.Bookings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace HotelHub.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);            
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        
        services.AddValidatorsFromAssembly(assembly);
        
        services.AddTransient<PricingService>();
        
        return services;
    }
}