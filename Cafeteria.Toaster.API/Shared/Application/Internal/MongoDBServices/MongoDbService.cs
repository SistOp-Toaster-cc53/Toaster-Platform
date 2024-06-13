using Cafeteria.Toaster.API.Shared.Domain.Model.Settings;
using Cafeteria.Toaster.API.Social.Domain.Model.Aggregates;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Cafeteria.Toaster.API.Shared.Application.Internal.MongoDBServices;

public class MongoDbService
{
    private readonly IMongoCollection<UserReference> _userReferencesCollection;

    public MongoDbService(IOptions<MongoDbSettings> mongoDbSettings)
    {
        MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _userReferencesCollection = database.GetCollection<UserReference>(mongoDbSettings.Value.CollectionName);
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
}