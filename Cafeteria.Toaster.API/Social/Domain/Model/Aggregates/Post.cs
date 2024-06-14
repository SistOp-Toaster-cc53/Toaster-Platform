using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cafeteria.Toaster.API.Social.Domain.Model.Aggregates;

public class Post
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public UserReference User { get; set; }
    public IEnumerable<string> Comments { get; set; }
    public IEnumerable<string> Toasts { get; set; }
    public IEnumerable<string> Likes { get; set; }
    public int LikeCount { get; set; }
    public string Content { get; set; } = null!;
    public string Image { get; set; } = null!;
    public DateTime DateCreated { get; set; }
}