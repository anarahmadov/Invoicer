using Invoicer.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Invoicer.Infrastructure;

public class InvoicerDbContext : IdentityDbContext<User>
{
    public InvoicerDbContext(DbContextOptions<InvoicerDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(InvoicerDbContext).Assembly);
    }
}
