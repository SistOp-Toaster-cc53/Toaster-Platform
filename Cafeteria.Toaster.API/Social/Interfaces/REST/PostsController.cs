﻿using Cafeteria.Toaster.API.Shared.Application.Internal.MongoDBServices;
using Cafeteria.Toaster.API.Social.Domain.Model.Aggregates;
using Cafeteria.Toaster.API.Social.Interfaces.REST.Resources;
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
    
    [HttpGet("/author/{author}")]
    public async Task<List<Post>> GetPostsByAuthor(string author)
    {
        var post = await _mongoDbService.GetPostsByAuthor(author);
        return post;
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
    
    [HttpPut("{id}/like")]
    public async Task<IActionResult> changeLikes(string id, [FromBody] EditLikesResource resource)
    {
        var post = await _mongoDbService.GetPostByIdAsync(id);
        if (post == null)
        {
            return NotFound();
        }
        
        // Work with the original collection directly
        var likesList = post.Likes.ToList();
        
        if (!likesList.Contains(resource.Username))
        {
            post.LikeCount++;
            likesList.Add(resource.Username);
        }
        else
        {
            post.LikeCount--;
            likesList.Remove(resource.Username);
        }

        post.Likes = likesList; // Update the list of likes
        await _mongoDbService.UpdatePostLikesAsync(id, post);
        return NoContent();
    }
}