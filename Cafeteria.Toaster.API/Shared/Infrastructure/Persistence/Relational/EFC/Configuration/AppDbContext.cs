using ACME.LearningCenterPlatform.API.IAM.Domain.Model.Aggregates;
using Cafeteria.Toaster.API.Profiles.Domain.Model.Aggregates;
using Cafeteria.Toaster.API.Profiles.Domain.Model.Entities;
using Cafeteria.Toaster.API.Shared.Infrastructure.Persistence.Relational.EFC.Configuration.Extensions;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Cafeteria.Toaster.API.Shared.Infrastructure.Persistence.Relational.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        base.OnConfiguring(builder);
        // Enable Audit Fields Interceptors
        builder.AddCreatedUpdatedInterceptor();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Profiles Bounded Context
        builder.Entity<Profile>().HasKey(p => p.Id);
        builder.Entity<Profile>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Profile>().Property(p => p.Username).IsRequired();
        builder.Entity<Profile>().Property(p => p.ProfileUrl).IsRequired();
        builder.Entity<Profile>().Property(p => p.BackgroundUrl).IsRequired();
        builder.Entity<Profile>().Property(p => p.Description).IsRequired();
        
        builder.Entity<Follow>()
            .HasKey(pf => new { pf.FollowerId, pf.InfluencerId });

        builder.Entity<Follow>()
            .HasOne(pf => pf.Follower)
            .WithMany(p => p.Followers)
            .HasForeignKey(pf => pf.FollowerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Follow>()
            .HasOne(pf => pf.Influencer)
            .WithMany(p => p.Influencers)
            .HasForeignKey(pf => pf.InfluencerId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(u => u.Username).IsRequired();
        builder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
        
        // Apply SnakeCase Naming Convention
        builder.UseSnakeCaseWithPluralizedTableNamingConvention();
    }
}