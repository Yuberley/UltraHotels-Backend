using HotelHub.Application.Abstractions.Authentication;
using HotelHub.Application.Abstractions.Messaging;
using HotelHub.Domain.Abstractions;
using HotelHub.Domain.Users;

namespace HotelHub.Application.Users.LogIn;

internal sealed class LogInUserCommandHandler : ICommandHandler<LogInUserCommand, AccessTokenResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IHashingService _hashingService;
    
    public LogInUserCommandHandler(IUserRepository userRepository, IJwtProvider jwtProvider, IHashingService hashingService)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
        _hashingService = hashingService;
    }
    
    public async Task<Result<AccessTokenResponse>> Handle(LogInUserCommand request, CancellationToken cancellationToken)
    {
        Email emailResult = new Email(request.Email);
        User? user = await _userRepository.GetByEmailAsync(emailResult, cancellationToken);
        
        if (user is null)
        {
            return Result.Failure<AccessTokenResponse>(UserErrors.NotFound);
        }
        
        if (!_hashingService.VerifyPassword(user.Password.Value, request.Password))
        {
            return Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials);
        }
        
        string token = _jwtProvider.GenerateToken(user);
        
        return Result.Success(new AccessTokenResponse(token));
    }
}