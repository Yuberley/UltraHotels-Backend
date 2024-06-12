using HotelHub.Domain.Users;

namespace HotelHub.Application.Abstractions.Authentication;

public interface IJwtProvider
{
    string GenerateToken(User user);
}