using HotelHub.Domain.Abstractions;
using HotelHub.Domain.SharedValueObjects;

namespace HotelHub.Domain.Users;

public sealed class User : Entity
{
    public User(
        Guid id,
        Email email, 
        Password password,
        Role role) : base(id)
    {
        Email = email;
        Password = password;
        Role = role;
        CreatedAt = DateTime.UtcNow;
    }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public Role Role { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private User()
    {
    }
    
}