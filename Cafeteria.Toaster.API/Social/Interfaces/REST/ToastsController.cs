using Cafeteria.Toaster.API.Shared.Application.Internal.MongoDBServices;
using Cafeteria.Toaster.API.Social.Domain.Model.Aggregates;
using Microsoft.AspNetCore.Mvc;

namespace Cafeteria.Toaster.API.Social.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
public class ToastsController : ControllerBase
{
    private readonly MongoDbService _mongoDbService;

    public ToastsController(MongoDbService mongoDbService)
    {
        _mongoDbService = mongoDbService;
    }
    
    [HttpGet]
    public async Task<List<Toast>> GetAll()
    {
        return await _mongoDbService.GetAllToastsAsync();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateToast([FromBody] Toast toast)
    {
        await _mongoDbService.CreateToastAsync(toast);
        return CreatedAtAction(nameof(GetAll), new { id = toast.Id }, toast);
    }

}