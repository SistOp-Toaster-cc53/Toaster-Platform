using Cafeteria.Toaster.API.Profiles.Domain.Model.Aggregates;
using Cafeteria.Toaster.API.Profiles.Domain.Model.Queries;

namespace Cafeteria.Toaster.API.Profiles.Domain.Services;

public interface IProfileQueryService
{
    Task<Profile?> Handle(GetProfileByUsernameQuery query);
    Task<Profile?> Handle(GetProfileByIdQuery query);
}