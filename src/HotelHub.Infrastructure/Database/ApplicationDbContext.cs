using HotelHub.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace HotelHub.Infrastructure.Database;

public class ApplicationDbContext : DbContext, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // This method ensures that changes to the database are saved as a single atomic transaction.
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}