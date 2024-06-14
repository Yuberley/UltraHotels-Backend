using HotelHub.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelHub.Infrastructure.Database.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        
        builder.HasKey(user => user.Id);
        
        builder.Property(user => user.Email)
            .HasConversion(
                email => email.Value,
                value => new Domain.SharedValueObjects.Email(value))
            .HasMaxLength(100);
        builder.HasIndex(user => user.Email).IsUnique();
        
        
        builder.Property(user => user.Password)
            .HasConversion(
                passwordHash => passwordHash.Value,
                value => new Password(value))
            .HasMaxLength(200);
        
        builder.Property(user => user.Role)
            .HasConversion(
                role => role.Value,
                value => Role.FromValue(value))
            .HasMaxLength(50);
    }
}