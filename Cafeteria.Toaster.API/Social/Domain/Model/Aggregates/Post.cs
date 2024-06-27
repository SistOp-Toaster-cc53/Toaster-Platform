using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cafeteria.Toaster.API.Social.Domain.Model.Aggregates;

public class Post
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; } = null!;
    public UserReference User { get; set; } = null!;
    public IEnumerable<string> Comments { get; set; }= null!;
    public IEnumerable<string> Toasts { get; set; }= null!;
    public IEnumerable<string> Likes { get; set; }= null!;
    public int LikeCount { get; set; } 
    public string Content { get; set; } = null!;
    public string Image { get; set; } = null!;
    public DateTime DateCreated { get; set; }
}