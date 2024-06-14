using HotelHub.Application.Abstractions.Authentication;
using HotelHub.Domain.Abstractions;
using HotelHub.Domain.SharedValueObjects;
using HotelHub.Domain.Users;
using MediatR;

namespace HotelHub.Application.Users.Register;

internal sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IHashingService _hashingService;
    
    public RegisterUserCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IHashingService hashingService)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _hashingService = hashingService;
    }
    
    
    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByEmailAsync(new Email(request.Email), cancellationToken);

        if (existingUser is not null)
        {
            return Result.Failure<Guid>(UserErrors.EmailAlreadyExists(request.Email));
        }
        
        var hashedPassword = _hashingService.HashPassword(request.Password);

        var user = new User(
            Guid.NewGuid(),
            new Email(request.Email),
            new Password(hashedPassword),
            Role.Default
        );
        
        _userRepository.Add(user);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return user.Id;
    }
}