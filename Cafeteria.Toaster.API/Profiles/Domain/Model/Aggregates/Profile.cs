using Cafeteria.Toaster.API.Profiles.Domain.Model.Commands;
using Cafeteria.Toaster.API.Profiles.Domain.Model.Entities;

namespace Cafeteria.Toaster.API.Profiles.Domain.Model.Aggregates;

public partial class Profile
{
    public Profile()
    {
        Username = "";
        Followers = new List<Follow>();
        Influencers = new List<Follow>();
        ProfileUrl = "";
        Description = "";
        BackgroundUrl = "";
    }
    
    public Profile(CreateProfileCommand command)
    {
        Username = command.Username;
        Followers = new List<Follow>();
        Influencers = new List<Follow>();
        ProfileUrl = "";
        Description = "Hi, I'm new here!";
        BackgroundUrl = "";
    }
    
    public int Id { get; set; }
    public string Username { get; set; }

    public ICollection<Follow> Followers { get; set; }
    public ICollection<Follow> Influencers { get; set; }
    
    public string ProfileUrl { get; set; }
    
    public string Description { get; set; }
    
    public string BackgroundUrl { get; set; }
}