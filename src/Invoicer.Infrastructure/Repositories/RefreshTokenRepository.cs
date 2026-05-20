using Invoicer.Application.Services;
using Invoicer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Invoicer.Infrastructure.Repositories;

public sealed class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly InvoicerDbContext _dbContext;

    public RefreshTokenRepository(InvoicerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(RefreshToken refreshToken)
    {
        _dbContext.RefreshTokens.Add(refreshToken);
    }

    public Task<RefreshToken?> GetByTokenAsync(string token)
    {
        return _dbContext.RefreshTokens.SingleOrDefaultAsync(rt => rt.Token == token);
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
