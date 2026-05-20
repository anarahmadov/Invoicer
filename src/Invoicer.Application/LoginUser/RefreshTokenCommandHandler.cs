using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Invoicer.Application.ResultPattern;
using Invoicer.Application.Services;
using Invoicer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Invoicer.Application.LoginUser;

public sealed class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<AuthenticationResult>>
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly JwtSettings _jwtSettings;

    public RefreshTokenCommandHandler(
        UserManager<User> userManager,
        IJwtTokenGenerator jwtTokenGenerator,
        IRefreshTokenRepository refreshTokenRepository,
        IOptions<JwtSettings> jwtSettings)
    {
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
        _refreshTokenRepository = refreshTokenRepository;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<Result<AuthenticationResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var principal = GetPrincipalFromToken(request.AccessToken);
        if (principal is null)
        {
            return Result<AuthenticationResult>.Failure("Invalid access token.");
        }

        var jwtId = principal.FindFirstValue(JwtRegisteredClaimNames.Jti);
        var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(jwtId) || string.IsNullOrWhiteSpace(userId))
        {
            return Result<AuthenticationResult>.Failure("Invalid access token.");
        }

        var storedToken = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken);

        if (storedToken is null)
        {
            return Result<AuthenticationResult>.Failure("Refresh token does not exist.");
        }

        if (storedToken.IsRevoked || storedToken.IsUsed)
        {
            return Result<AuthenticationResult>.Failure("Refresh token is invalid.");
        }

        if (storedToken.ExpiresAt < DateTime.UtcNow)
        {
            return Result<AuthenticationResult>.Failure("Refresh token has expired.");
        }

        if (storedToken.JwtId != jwtId || storedToken.UserId.ToString() != userId)
        {
            return Result<AuthenticationResult>.Failure("Refresh token does not match access token.");
        }

        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is null)
        {
            return Result<AuthenticationResult>.Failure("User not found.");
        }

        storedToken.IsUsed = true;
        await _refreshTokenRepository.SaveChangesAsync(cancellationToken);

        var authResult = _jwtTokenGenerator.GenerateToken(user);
        return Result<AuthenticationResult>.Success(authResult);
    }

    private ClaimsPrincipal? GetPrincipalFromToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
                ValidateIssuer = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = _jwtSettings.Audience,
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero
            };

            var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
            if (validatedToken is 
                JwtSecurityToken jwtToken &&
                jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return principal;
            }

            return null;
        }
        catch
        {
            return null;
        }
    }
}
