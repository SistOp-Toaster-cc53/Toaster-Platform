using Cafeteria.Toaster.API.Shared.Application.Internal.MongoDBServices;
using Cafeteria.Toaster.API.Social.Domain.Model.Aggregates;
using Microsoft.AspNetCore.Mvc;

namespace Cafeteria.Toaster.API.Social.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly MongoDbService _mongoDbService;

    public CommentsController(MongoDbService mongoDbService)
    {
        _mongoDbService = mongoDbService;
    }
    
    [HttpGet]
    public async Task<List<Comment>> GetAll()
    {
        return await _mongoDbService.GetAllComentsAsync();
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment([FromBody] Comment comment)
    {
        await _mongoDbService.CreateCommentAsync(comment);
        return CreatedAtAction(nameof(GetAll), new { id = comment.Id }, comment);
    }
}