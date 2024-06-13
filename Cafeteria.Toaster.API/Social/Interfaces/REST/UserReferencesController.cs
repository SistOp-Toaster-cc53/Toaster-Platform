using System.Net.Mime;
using Cafeteria.Toaster.API.Shared.Application.Internal.MongoDBServices;
using Cafeteria.Toaster.API.Social.Domain.Model.Aggregates;
using Microsoft.AspNetCore.Mvc;

namespace Cafeteria.Toaster.API.Social.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
public class UserReferencesController : ControllerBase
{
    private readonly MongoDbService _mongoDbService;

    public UserReferencesController(MongoDbService mongoDbService)
    {
        _mongoDbService = mongoDbService;
    }

    [HttpGet]
    public async Task<List<UserReference>> GetAll()
    {
        return await _mongoDbService.GetAllUserReferencesAsync();
    }

    [HttpPost]
    public async Task<IActionResult> CreateUserReference([FromBody] UserReference userReference)
    {
        await _mongoDbService.CreateUserReferenceAsync(userReference);
        return CreatedAtAction(nameof(GetAll), new { id = userReference.Id }, userReference);
    }
}