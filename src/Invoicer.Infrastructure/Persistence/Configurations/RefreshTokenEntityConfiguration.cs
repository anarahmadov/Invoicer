using Invoicer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invoicer.Infrastructure.Persistence.Configurations;

public class RefreshTokenEntityConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(rt => rt.Id);

        builder.HasIndex(rt => rt.Token).IsUnique();

        builder.Property(rt => rt.Token).IsRequired();

        builder.Property(rt => rt.JwtId).IsRequired();

        builder.Property(rt => rt.UserId).IsRequired();

        builder.HasOne(rt => rt.User)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
