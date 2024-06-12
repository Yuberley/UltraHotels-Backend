namespace HotelHub.WebAPI.Controllers.User;

public sealed record RegisterRequest(
    string Email,
    string Password,
    string Role
    );