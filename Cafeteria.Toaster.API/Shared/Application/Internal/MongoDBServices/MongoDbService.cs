using Cafeteria.Toaster.API.Shared.Domain.Model.Settings;
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
    
    public async Task CreateCommentAsync(Comment comment)
    {
        await _commentsCollection.InsertOneAsync(comment);
        return;
    }

    public async Task<List<Comment>> GetAllComentsAsync()
    {
        return await _commentsCollection.Find(new BsonDocument()).ToListAsync();
    }
}