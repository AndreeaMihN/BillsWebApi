using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Bill.Domain.Users;

[BsonIgnoreExtraElements]
public class User
{
    [BsonId]
    //[BsonRepresentation(BsonType.ObjectId)]
    [JsonConverter(typeof(ObjectIdConverter))]
    public Guid? Id { get; set; }

    [BsonElement("userName")]
    [Required]
    public string UserName { get; set; } = string.Empty;

    [BsonElement("email")]
    public string Email { get; set; } = string.Empty;
    [BsonElement("password")]
    public string Password { get; set; } = string.Empty;

    [BsonElement("personalIdentificationNumber")]
    public string PersonalIdentificationNumber { get; set; } = string.Empty;

    [BsonElement("status")]
    public bool IsActive { get; set; }
}