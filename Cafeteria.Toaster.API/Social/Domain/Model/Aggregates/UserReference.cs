using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cafeteria.Toaster.API.Social.Domain.Model.Aggregates;

public class UserReference
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Toaster { get; set; }
    public string Username { get; set; }
    public string Image { get; set; } = null!;
    public bool Verified { get; set; }
}