using Cafeteria.Toaster.API.Profiles.Domain.Model.Commands;
using Cafeteria.Toaster.API.Profiles.Domain.Services;

namespace Cafeteria.Toaster.API.Profiles.Interfaces.ACL.Services;

public class ProfilesContextFacade(IProfileCommandService profileCommandService, IProfileQueryService profileQueryService) : IProfilesContextFacade
{
    public async Task<int> CreateProfile(string username)
    {
        var createProfileCommand = new CreateProfileCommand(username);
        var profile = await profileCommandService.Handle(createProfileCommand);
        return profile?.Id ?? 0;
    }
}