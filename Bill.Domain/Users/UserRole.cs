using Microsoft.AspNetCore.Identity;

namespace Bill.Domain.Users;

public class UserRole : IdentityUserRole<Guid>
{
    public string RoleName { get; set; }
}
