using Cafeteria.Toaster.API.Profiles.Domain.Model.Commands;
using Cafeteria.Toaster.API.Profiles.Interfaces.REST.Resources;

namespace Cafeteria.Toaster.API.Profiles.Interfaces.REST.Transform;

public static class CreateProfileCommandFromResourceAssembler
{
    public static CreateProfileCommand ToCommandFromResource(CreateProfileResource resource)
    {
        return new CreateProfileCommand(resource.Username);
    }
}