using Microsoft.AspNetCore.Identity;

namespace Invoicer.Domain.Entities;

public class User : IdentityUser
{
    public string Fullname { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool IsDeleted { get; set; }
}
