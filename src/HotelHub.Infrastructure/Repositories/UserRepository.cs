using HotelHub.Domain.Users;
using HotelHub.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace HotelHub.Infrastructure.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    
    
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    
    public Task<User?> GetByEmailAsync(Domain.Users.Email email, CancellationToken cancellationToken = default)
    {
        return _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }
    
    public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }
    
    public void Add(User user)
    {
        _context.Users.Add(user);
    }
}