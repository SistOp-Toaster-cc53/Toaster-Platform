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
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(string id)
    {
        var result = await _mongoDbService.DeletePostAsync(id);
        if (result)
        {
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }
    
    [HttpGet("{commentId}")]
    public async Task<ActionResult<Post>> GetPostByCommentId(string commentId)
    {
        var post = await _mongoDbService.GetPostByCommentIdAsync(commentId);

        if (post == null)
        {
            return NotFound();
        }

        return post;
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(string id, [FromBody] Post post)
    {
        var existingPost = await _mongoDbService.GetPostByIdAsync(id);
        if (existingPost == null)
        {
            return NotFound();
        }

        if (post.Id != id)
        {
            return BadRequest("The id in the path and the id in the post body must be the same.");
        }

        await _mongoDbService.UpdatePostAsync(id, post);
        return NoContent();
    }
}