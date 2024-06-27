using Cafeteria.Toaster.API.Profiles.Domain.Model.Aggregates;
using Cafeteria.Toaster.API.Profiles.Domain.Repositories;
using Cafeteria.Toaster.API.Shared.Infrastructure.Persistence.Relational.EFC.Configuration;
using Cafeteria.Toaster.API.Shared.Infrastructure.Persistence.Relational.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cafeteria.Toaster.API.Profiles.Infrastructure.Persistence.EFC.Repositories;

public class ProfileRepository(AppDbContext context) : BaseRepository<Profile>(context), IProfileRepository
{
    public Task<Profile?> FindProfileByUsernameAsync(string username)
    {
        return Context.Set<Profile>().Where(p => p.Username == username).FirstOrDefaultAsync();
    }
}