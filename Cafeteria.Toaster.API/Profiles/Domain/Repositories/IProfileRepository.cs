using Cafeteria.Toaster.API.Profiles.Domain.Model.Aggregates;
using Cafeteria.Toaster.API.Shared.Domain.Repositories;

namespace Cafeteria.Toaster.API.Profiles.Domain.Repositories;

public interface IProfileRepository : IBaseRepository<Profile>
{
    Task<Profile?> FindProfileByUsernameAsync(string username);
}