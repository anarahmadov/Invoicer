using Microsoft.AspNetCore.Identity;

namespace Invoicer.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public string Fullname { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool IsDeleted { get; set; }

    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}
