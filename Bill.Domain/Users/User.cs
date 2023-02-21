using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Bill.Domain.Users;

[BsonIgnoreExtraElements]
public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; } = string.Empty;

    [BsonElement("firstName")]
    [Required]
    public string FirstName { get; set; } = string.Empty;

    [BsonElement("lastName")]
    public string LastName { get; set; } = string.Empty;

    [BsonElement("email")]
    public string Email { get; set; } = string.Empty;
    [BsonElement("password")]
    public string Password { get; set; } = string.Empty;

    [BsonElement("personalIdentificationNumber")]
    public string PersonalIdentificationNumber { get; set; } = string.Empty;

    [BsonElement("status")]
    public bool IsActive { get; set; }
}