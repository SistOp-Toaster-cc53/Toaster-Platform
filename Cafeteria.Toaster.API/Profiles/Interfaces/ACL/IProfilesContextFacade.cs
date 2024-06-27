namespace Cafeteria.Toaster.API.Profiles.Interfaces.ACL;

public interface IProfilesContextFacade
{
    Task<int> CreateProfile(string username);
}