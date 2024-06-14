using Cafeteria.Toaster.API.Shared.Application.Internal.MongoDBServices;
using Cafeteria.Toaster.API.Social.Domain.Model.Aggregates;
using Microsoft.AspNetCore.Mvc;

namespace Cafeteria.Toaster.API.Social.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
public class PostsController : ControllerBase
{
    private readonly MongoDbService _mongoDbService;

    public PostsController(MongoDbService mongoDbService)
    {
        _mongoDbService = mongoDbService;
    }
    
    [HttpGet]
    public async Task<List<Post>> GetAll()
    {
        return await _mongoDbService.GetAllPostsAsync();
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] Post post)
    {
        await _mongoDbService.CreatePostAsync(post);
        return CreatedAtAction(nameof(GetAll), new { id = post.Id }, post);
    }
}