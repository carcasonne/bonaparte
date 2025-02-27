using Bonaparte.Core.Identity;
using Bonaparte.Core.JoinEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bonaparte.Core.EntityFramework;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Game> Games { get; set; }
    public DbSet<Ruleset> RuleSets { get; set; }
    public DbSet<Achievement> Achievements { get; set; }
    public DbSet<Playthrough> Playthroughs { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configure Game entity relationships
        modelBuilder.Entity<Game>()
            .HasMany(g => g.Playthroughs)
            .WithOne(p => p.Game)
            .HasForeignKey(p => p.GameId)
            .OnDelete(DeleteBehavior.Cascade);
            
        // Configure Ruleset entity relationships
        modelBuilder.Entity<Ruleset>()
            .HasOne(r => r.Game)
            .WithMany()
            .HasForeignKey(r => r.GameId)
            .OnDelete(DeleteBehavior.Cascade);
            
        modelBuilder.Entity<Ruleset>()
            .HasOne(r => r.Owner)
            .WithMany()
            .HasForeignKey(r => r.OwnerId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);
            
        modelBuilder.Entity<Ruleset>()
            .HasMany(r => r.Playthroughs)
            .WithOne(p => p.Ruleset)
            .HasForeignKey(p => p.RulesetId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // Configure Achievement entity relationships
        modelBuilder.Entity<Achievement>()
            .HasOne(a => a.Owner)
            .WithMany()
            .HasForeignKey(a => a.OwnerId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);
            
        // Configure Playthrough entity relationships
        modelBuilder.Entity<Playthrough>()
            .HasOne(p => p.Owner)
            .WithMany()
            .HasForeignKey(p => p.OwnerId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);
        
        // Configure RulesetCoOwner many-to-many relationship
        modelBuilder.Entity<RulesetCoOwner>()
            .HasKey(rc => new { rc.RulesetId, rc.UserId });
            
        modelBuilder.Entity<RulesetCoOwner>()
            .HasOne(rc => rc.Ruleset)
            .WithMany(r => r.CoOwners)
            .HasForeignKey(rc => rc.RulesetId)
            .OnDelete(DeleteBehavior.Cascade);
            
        modelBuilder.Entity<RulesetCoOwner>()
            .HasOne(rc => rc.User)
            .WithMany(u => u.CoOwnedRulesets)
            .HasForeignKey(rc => rc.UserId)
            .OnDelete(DeleteBehavior.Cascade); // This will delete the join entry when user is deleted
            
        // Configure RulesetAchievement many-to-many relationship
        modelBuilder.Entity<RulesetAchievement>()
            .HasKey(ra => new { ra.RulesetId, ra.AchievementId });
            
        modelBuilder.Entity<RulesetAchievement>()
            .HasOne(ra => ra.Ruleset)
            .WithMany(r => r.Achievements)
            .HasForeignKey(ra => ra.RulesetId)
            .OnDelete(DeleteBehavior.Cascade);
            
        modelBuilder.Entity<RulesetAchievement>()
            .HasOne(ra => ra.Achievement)
            .WithMany(a => a.Rulesets)
            .HasForeignKey(ra => ra.AchievementId)
            .OnDelete(DeleteBehavior.Cascade);
            
        // Configure PlaythroughPlayer many-to-many relationship
        modelBuilder.Entity<PlaythroughPlayer>()
            .HasKey(pp => new { pp.PlaythroughId, pp.UserId });
            
        modelBuilder.Entity<PlaythroughPlayer>()
            .HasOne(pp => pp.Playthrough)
            .WithMany(p => p.Players)
            .HasForeignKey(pp => pp.PlaythroughId)
            .OnDelete(DeleteBehavior.Cascade);
            
        modelBuilder.Entity<PlaythroughPlayer>()
            .HasOne(pp => pp.User)
            .WithMany()
            .HasForeignKey(pp => pp.UserId)
            .OnDelete(DeleteBehavior.Cascade); // This will delete only the player join entry
            
        // Configure UserAchievement many-to-many relationship
        modelBuilder.Entity<UserAchievement>()
            .HasKey(ua => ua.Id);
            
        modelBuilder.Entity<UserAchievement>()
            .HasOne(ua => ua.User)
            .WithMany()
            .HasForeignKey(ua => ua.UserId)
            .OnDelete(DeleteBehavior.Cascade); // This removes user achievements when a user is deleted
            
        modelBuilder.Entity<UserAchievement>()
            .HasOne(ua => ua.Achievement)
            .WithMany(a => a.CompletedBy)
            .HasForeignKey(ua => ua.AchievementId)
            .OnDelete(DeleteBehavior.Cascade);
            
        modelBuilder.Entity<UserAchievement>()
            .HasOne(ua => ua.Playthrough)
            .WithMany(p => p.CompletedAchievements)
            .HasForeignKey(ua => ua.PlaythroughId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

