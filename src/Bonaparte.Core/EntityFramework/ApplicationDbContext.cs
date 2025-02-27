using Bonaparte.Core.Identity;
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
}