using System.Net.Mime;
using Cafeteria.Toaster.API.Profiles.Domain.Model.Queries;
using Cafeteria.Toaster.API.Profiles.Domain.Services;
using Cafeteria.Toaster.API.Profiles.Interfaces.REST.Resources;
using Cafeteria.Toaster.API.Profiles.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace Cafeteria.Toaster.API.Profiles.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class ProfilesController(IProfileCommandService profileCommandService, IProfileQueryService profileQueryService)
    : ControllerBase
{
    [HttpGet("{profileId:int}")]
    public async Task<IActionResult> GetProfileById(int profileId)
    {
        var getProfileByIdQuery = new GetProfileByIdQuery(profileId);
        var profile = await profileQueryService.Handle(getProfileByIdQuery);
        if (profile == null) return NotFound();
        var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
        return Ok(profileResource);
    }
    
    [HttpGet("username/{username}")]
    public async Task<IActionResult> GetProfileByUsername(string username)
    {
        var getProfileByUsernameQuery = new GetProfileByUsernameQuery(username);
        var profile = await profileQueryService.Handle(getProfileByUsernameQuery);
        if (profile == null) return NotFound();
        var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
        return Ok(profileResource);
    }
    
    [HttpPut]
    public async Task<IActionResult> EditProfile(EditProfileResource editProfileResource)
    {
        var editProfileCommand = EditProfileCommandFromResourceAssembler.ToCommandFromResource(editProfileResource);
        var profile = await profileCommandService.Handle(editProfileCommand);
        if (profile == null) return BadRequest();
        var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
        return Ok(profileResource);
    }
    
}