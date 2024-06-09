using HotelHub.Application.Abstractions.Messaging;

namespace HotelHub.Application.Users.LoginUser;

public sealed record LogInUserCommand(string Email, string Password)
    : ICommand<AccessTokenResponse>;