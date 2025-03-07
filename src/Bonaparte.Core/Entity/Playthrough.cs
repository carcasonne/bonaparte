using Bonaparte.Core.Common;
using Bonaparte.Core.Identity;
using Bonaparte.Core.JoinEntities;

namespace Bonaparte.Core;

// A playthrough is a session of playing some game with some ruleset 
public class Playthrough : AuditableEntity
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    
    public int GameId { get; set; }
    public Game Game { get; set; }
    public string OwnerId { get; set; }
    public ApplicationUser Owner { get; set; }
    public int RulesetId { get; set; }
    public Ruleset Ruleset { get; set; }
    
    public ICollection<PlaythroughPlayer> Players { get; set; } = new List<PlaythroughPlayer>();
    public ICollection<UserAchievement> CompletedAchievements { get; set; } = new List<UserAchievement>();
}