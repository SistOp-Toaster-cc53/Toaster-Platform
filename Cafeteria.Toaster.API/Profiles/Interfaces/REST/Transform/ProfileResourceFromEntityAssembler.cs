using Cafeteria.Toaster.API.Profiles.Domain.Model.Aggregates;
using Cafeteria.Toaster.API.Profiles.Interfaces.REST.Resources;

namespace Cafeteria.Toaster.API.Profiles.Interfaces.REST.Transform;

public static class ProfileResourceFromEntityAssembler
{
    public static ProfileResource ToResourceFromEntity(Profile entity)
    {
        return new ProfileResource(entity.Id, entity.Username, entity.ProfileUrl, entity.Description, entity.BackgroundUrl);
    }
}