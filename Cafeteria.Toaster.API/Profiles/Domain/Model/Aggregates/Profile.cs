using Cafeteria.Toaster.API.Profiles.Domain.Model.Entities;

namespace Cafeteria.Toaster.API.Profiles.Domain.Model.Aggregates;

public partial class Profile
{
    public int Id { get; set; }
    public string Username { get; set; }

    public ICollection<Follow> Followers { get; set; }
    public ICollection<Follow> Influencers { get; set; }
}