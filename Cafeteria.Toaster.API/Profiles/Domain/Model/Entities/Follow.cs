using Cafeteria.Toaster.API.Profiles.Domain.Model.Aggregates;

namespace Cafeteria.Toaster.API.Profiles.Domain.Model.Entities;

public class Follow
{
    public int FollowerId { get; set; }
    public Profile Follower { get; set; }

    public int InfluencerId { get; set; }
    public Profile Influencer { get; set; }
}