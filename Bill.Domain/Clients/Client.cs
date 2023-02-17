using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Bill.Domain.Clients;

[BsonIgnoreExtraElements]
public class Client
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; } = string.Empty;

    [BsonElement("firstName")]
    public string FirstName { get; set; } = string.Empty;

    [BsonElement("lastName")]
    public string LastName { get; set; } = string.Empty;

    [BsonElement("email")]
    public string Email { get; set; } = string.Empty;

    [BsonElement("personalIdentificationNumber")]
    public string PersonalIdentificationNumber { get; set; } = string.Empty;

    [BsonElement("status")]
    public bool IsActive { get; set; }
}