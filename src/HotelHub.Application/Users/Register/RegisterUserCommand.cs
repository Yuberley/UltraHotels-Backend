using HotelHub.Application.Abstractions.Messaging;

namespace HotelHub.Application.Users.Register;

public sealed record RegisterUserCommand(  
    string Email,
    string Password,
    string Role) : ICommand<Guid>;