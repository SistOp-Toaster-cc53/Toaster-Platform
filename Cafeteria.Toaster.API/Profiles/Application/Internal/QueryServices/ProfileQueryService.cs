using Cafeteria.Toaster.API.Profiles.Domain.Model.Aggregates;
using Cafeteria.Toaster.API.Profiles.Domain.Model.Queries;
using Cafeteria.Toaster.API.Profiles.Domain.Repositories;
using Cafeteria.Toaster.API.Profiles.Domain.Services;

namespace Cafeteria.Toaster.API.Profiles.Application.Internal.QueryServices;

public class ProfileQueryService(IProfileRepository profileRepository) : IProfileQueryService
{
    public async Task<Profile?> Handle(GetProfileByUsernameQuery query)
    {
        return await profileRepository.FindProfileByUsernameAsync(query.Username);
    }

    public async Task<Profile?> Handle(GetProfileByIdQuery query)
    {
        return await profileRepository.FindByIdAsync(query.Id);
    }
}