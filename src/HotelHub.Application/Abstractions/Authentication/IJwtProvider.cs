using System.IdentityModel.Tokens.Jwt;
using HotelHub.Domain.Users;

namespace HotelHub.Application.Abstractions.Authentication;

public interface IJwtProvider
{
    string GenerateToken(User user);
    JwtSecurityToken DecodeToken(string? token);
}