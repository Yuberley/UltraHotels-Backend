using HotelHub.Application.Abstractions.Clock;
using HotelHub.Application.Exceptions;
using HotelHub.Domain.Abstractions;
using HotelHub.Domain.Bookings;
using HotelHub.Domain.Guests;
using HotelHub.Domain.Hotels;
using HotelHub.Domain.Rooms;
using HotelHub.Domain.Users;
using HotelHub.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HotelHub.Infrastructure.Database;

public class ApplicationDbContext : DbContext, IUnitOfWork
{
    private readonly IDateTimeProvider _dateTimeProvider;
    
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IDateTimeProvider dateTimeProvider) : base(options)
    {
        _dateTimeProvider = dateTimeProvider;
    }
    
    
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Guest> Guests { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    
    public DbSet<User> Users { get; set; }
    
    // Sobreescribimos el método OnModelCreating para aplicar las configuraciones de las entidades.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
    
    
    // Sobreescribimos el método SaveChangesAsync del DbContext, este método garantiza
    // que los cambios en la base de datos se guarden como una única transacción atómica.
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            AddDomainEventsAsOutboxMessages();
            
            int result = await base.SaveChangesAsync(cancellationToken);
            
            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("Concurrency exception occurred.", ex);
        }
    }
    
    
    /// <summary>
    /// Se encarga de pasar los eventos de dominio (IDomainEvent) generados por las entidades durante
    /// la operación en mensajes que se almacenarán en una tabla de "outbox". Estos mensajes se pueden
    /// procesar posteriormente, permitiendo un enfoque de entrega garantizada y comunicación entre
    /// diferentes partes del sistema.
    /// </summary>
    private void AddDomainEventsAsOutboxMessages()
    {
        var outboxMessages = ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                IReadOnlyList<IDomainEvent> domainEvents = entity.GetDomainEvents();
                
                entity.ClearDomainEvents();
                
                return domainEvents;
            })
            .Select(domainEvent => new OutboxMessage(
                Guid.NewGuid(),
                _dateTimeProvider.UtcNow,
                domainEvent.GetType().Name,
                JsonConvert.SerializeObject(domainEvent, JsonSerializerSettings)))
            .ToList();
        
        AddRange(outboxMessages);
    }
    
    private static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All
    };
}