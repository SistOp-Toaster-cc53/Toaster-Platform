using Cafeteria.Toaster.API.Profiles.Domain.Model.Aggregates;
using Cafeteria.Toaster.API.Profiles.Domain.Model.Commands;
using Cafeteria.Toaster.API.Profiles.Domain.Repositories;
using Cafeteria.Toaster.API.Profiles.Domain.Services;
using Cafeteria.Toaster.API.Shared.Domain.Repositories;

namespace Cafeteria.Toaster.API.Profiles.Application.Internal.CommandServices;

public class ProfileCommandService(IProfileRepository profileRepository, IUnitOfWork unitOfWork) : IProfileCommandService
{
    public async Task<Profile?> Handle(CreateProfileCommand command)
    {
        var profile = new Profile(command);
        try
        {
            await profileRepository.AddAsync(profile);
            await unitOfWork.CompleteAsync();
            return profile;
        } catch (Exception e)
        {
            Console.WriteLine($"An error occurred while creating the profile: {e.Message}");
            return null;
        }
    }
}