using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace Bill.Domain.Users;

[CollectionName("Roles")]
public class ApplicationRole : MongoIdentityRole<Guid>
{
}
