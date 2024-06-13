using HotelHub.Application.Abstractions.Messaging;

namespace HotelHub.Application.Users.Register;

public sealed record RegisterUserCommand(  
    string Email,
    string Password) : ICommand<Guid>;