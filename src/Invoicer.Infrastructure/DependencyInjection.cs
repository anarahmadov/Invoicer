using Invoicer.Application;
using Invoicer.Application.Services;
using Invoicer.Domain.Entities;
using Invoicer.Infrastructure.Repositories;
using Invoicer.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Invoicer.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<InvoicerDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddIdentityCore<User>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<InvoicerDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

        return services;
    }
}
