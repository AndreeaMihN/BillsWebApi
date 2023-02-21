using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace Bill.Domain.Users;

[CollectionName("Users")]
public class ApplicationUser : MongoIdentityUser<Guid>
{
}
