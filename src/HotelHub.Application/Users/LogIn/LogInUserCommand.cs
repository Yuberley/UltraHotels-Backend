using HotelHub.Application.Abstractions.Messaging;

namespace HotelHub.Application.Users.LogIn;

public sealed record LogInUserCommand(string Email, string Password)
    : ICommand<AccessTokenResponse>;