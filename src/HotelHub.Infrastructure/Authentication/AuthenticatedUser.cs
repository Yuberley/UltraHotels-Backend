using HotelHub.Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Http;

namespace HotelHub.Infrastructure.Authentication;

internal sealed class AuthenticatedUser : IAuthenticatedUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IJwtProvider _jwtProvider;
    
    public AuthenticatedUser(IHttpContextAccessor httpContextAccessor, IJwtProvider jwtProvider)
    {
        _httpContextAccessor = httpContextAccessor;
        _jwtProvider = jwtProvider;
    }
    
    public Guid GetUserId()
    {
        HttpContext? httpContext = _httpContextAccessor.HttpContext;
        IHeaderDictionary? headers = httpContext?.Request.Headers;
        
        if (headers != null && !headers.ContainsKey("Authorization"))
        {
            throw new UnauthorizedAccessException("Authorization header is missing");
        }
        
        string? token = headers?["Authorization"].ToString().Replace("Bearer ", string.Empty);
        
        var jwtToken = _jwtProvider.DecodeToken(token);
        
        Guid userId = Guid.Parse(jwtToken.Subject);
        
        return userId;
    }
}