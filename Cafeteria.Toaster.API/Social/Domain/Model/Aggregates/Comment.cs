﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cafeteria.Toaster.API.Social.Domain.Model.Aggregates;

public class Comment
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Username { get; set; }
    public string UserPhoto { get; set; }
    public UserReference User { get; set; }
    public IEnumerable<string> Comments { get; set; }
    public IEnumerable<string> Toasts { get; set; }
    public IEnumerable<string> Posts { get; set; }
    public IEnumerable<string> Likes { get; set; }
    public int LikeCount { get; set; }
    public string Content { get; set; } = null!;
    public string Image { get; set; } = null!;
    public DateTime DateCreated { get; set; }
    [BsonRepresentation(BsonType.ObjectId)]
    public string PostId { get; set; }
}