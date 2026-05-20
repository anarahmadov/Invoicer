namespace Invoicer.Application.Services;

public sealed class AuthenticationResult
{
    public string AccessToken { get; }
    public DateTime AccessTokenExpiresAt { get; }
    public string RefreshToken { get; }
    public DateTime RefreshTokenExpiresAt { get; }

    public AuthenticationResult(
        string accessToken,
        DateTime accessTokenExpiresAt,
        string refreshToken,
        DateTime refreshTokenExpiresAt)
    {
        AccessToken = accessToken;
        AccessTokenExpiresAt = accessTokenExpiresAt;
        RefreshToken = refreshToken;
        RefreshTokenExpiresAt = refreshTokenExpiresAt;
    }
}
