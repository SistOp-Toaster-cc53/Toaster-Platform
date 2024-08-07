﻿using Cafeteria.Toaster.API.Shared.Domain.Model.Settings;
using Cafeteria.Toaster.API.Social.Domain.Model.Aggregates;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Cafeteria.Toaster.API.Shared.Application.Internal.MongoDBServices;

public class MongoDbService
{
    private readonly IMongoCollection<UserReference> _userReferencesCollection;
    private readonly IMongoCollection<Toast> _toastsCollection;
    private readonly IMongoCollection<Post> _postsCollection;
    private readonly IMongoCollection<Comment> _commentsCollection;

    public MongoDbService(IOptions<MongoDbSettings> mongoDbSettings)
    {
        MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _userReferencesCollection = database.GetCollection<UserReference>(mongoDbSettings.Value.CollectionName);
        _toastsCollection = database.GetCollection<Toast>("toasts");
        _postsCollection = database.GetCollection<Post>("posts");
        _commentsCollection = database.GetCollection<Comment>("comments");
    }

    public async Task CreateUserReferenceAsync(UserReference userReference)
    {
        await _userReferencesCollection.InsertOneAsync(userReference);
        return;
    }

    public async Task<List<UserReference>> GetAllUserReferencesAsync()
    {
        return await _userReferencesCollection.Find(new BsonDocument()).ToListAsync();
    }
    
    public async Task CreateToastAsync(Toast toast)
    {
        await _toastsCollection.InsertOneAsync(toast);
        return;
    }

    public async Task<List<Toast>> GetAllToastsAsync()
    {
        return await _toastsCollection.Find(new BsonDocument()).ToListAsync();
    }
    
    public async Task CreatePostAsync(Post post)
    {
        await _postsCollection.InsertOneAsync(post);
        return;
    }

    public async Task<List<Post>> GetAllPostsAsync()
    {
        return await _postsCollection.Find(new BsonDocument()).ToListAsync();
    }
    
    public async Task<bool> DeletePostAsync(string id)
    {
        var filter = Builders<Post>.Filter.Eq(post => post.Id, id);
        var result = await _postsCollection.DeleteOneAsync(filter);
        return result.DeletedCount > 0;
    }
    
    public async Task CreateCommentAsync(Comment comment)
    {
        await _commentsCollection.InsertOneAsync(comment);
        return;
    }

    public async Task<List<Comment>> GetAllComentsAsync()
    {
        return await _commentsCollection.Find(new BsonDocument()).ToListAsync();
    }
    
    public async Task<Post> GetPostByIdAsync(string id)
    {
        var filter = Builders<Post>.Filter.Eq(post => post.Id, id);
        return await _postsCollection.Find(filter).FirstOrDefaultAsync();
    }
    
    public async Task<List<Comment>> GetCommentsByPostId(string postId)
    {
        var filter = Builders<Comment>.Filter.Eq(comment => comment.PostId, postId);
        return await _commentsCollection.Find(filter).ToListAsync();
    }
    
    public async Task<List<Post>> GetPostsByAuthor(string username)
    {
        var filter = Builders<Post>.Filter.Eq(post => post.User.Username, username);
        return await _postsCollection.Find(filter).ToListAsync();
    }
    
    public async Task<Post> GetPostByCommentIdAsync(string commentId)
    {
        var filter = Builders<Post>.Filter.AnyEq(post => post.Comments, commentId);
        return await _postsCollection.Find(filter).FirstOrDefaultAsync();
    }
    
    public async Task UpdatePostAsync(string id, Post post)
    {
        var filter = Builders<Post>.Filter.Eq(existingPost => existingPost.Id, id);
        await _postsCollection.ReplaceOneAsync(filter, post);
    }
    
    // change likes and likeCount
    public async Task UpdatePostLikesAsync(string id, Post post)
    {
        var filter = Builders<Post>.Filter.Eq(existingPost => existingPost.Id, id);
        var update = Builders<Post>.Update
            .Set(existingPost => existingPost.Likes, post.Likes)
            .Set(existingPost => existingPost.LikeCount, post.LikeCount);
        await _postsCollection.UpdateOneAsync(filter, update);
    }
}