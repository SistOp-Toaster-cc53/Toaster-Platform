using Cafeteria.Toaster.API.Profiles.Domain.Model.Commands;
using Cafeteria.Toaster.API.Profiles.Interfaces.REST.Resources;

namespace Cafeteria.Toaster.API.Profiles.Interfaces.REST.Transform;

public static class EditProfileCommandFromResourceAssembler
{
    public static EditProfileCommand ToCommandFromResource(EditProfileResource resource)
    {
        return new EditProfileCommand(resource.Id, resource.ProfileUrl, resource.Description, resource.BackgroundUrl);
    }
}