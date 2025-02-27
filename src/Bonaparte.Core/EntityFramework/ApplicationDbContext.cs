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
    
        modelBuilder.Entity<RulesetCoOwner>()
            .HasKey(rc => new { rc.RulesetId, rc.UserId });
        
        modelBuilder.Entity<RulesetCoOwner>()
            .HasOne(rc => rc.Ruleset)
            .WithMany(r => r.CoOwners)
            .HasForeignKey(rc => rc.RulesetId);
        
        modelBuilder.Entity<RulesetCoOwner>()
            .HasOne(rc => rc.User)
            .WithMany(u => u.CoOwnedRulesets)
            .HasForeignKey(rc => rc.UserId);
    }
}

