using System.Security.Cryptography;
using System.Text;
using HotelHub.Application.Abstractions.Authentication;

namespace HotelHub.Infrastructure.Authentication;

public class HashingService : IHashingService
{
    public string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
    
    public bool VerifyPassword(string hashedPassword, string password)
    {
        string hashOfInput = HashPassword(password);
        return StringComparer.OrdinalIgnoreCase.Compare(hashedPassword, hashOfInput) == 0;
    }
}