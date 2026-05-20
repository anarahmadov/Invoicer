using Invoicer.Domain.Entities;

namespace Invoicer.Application.Services;

public interface IRefreshTokenRepository
{
    void Add(RefreshToken refreshToken);
    Task<RefreshToken?> GetByTokenAsync(string token);
    void SaveChanges();
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
