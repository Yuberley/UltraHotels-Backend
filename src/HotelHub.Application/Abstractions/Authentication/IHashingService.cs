namespace HotelHub.Application.Abstractions.Authentication;

public interface IHashingService
{
    string HashPassword(string password);
    bool VerifyPassword(string hashedPassword, string password);
}