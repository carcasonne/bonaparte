using Bonaparte.Core.Common;
using Bonaparte.Core.Identity;
using Bonaparte.Core.JoinEntities;

namespace Bonaparte.Core;

// A ruleset is a list of achievements for a specific game
public class Ruleset : AuditableEntity
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public int GameId { get; set; }
    public Game Game { get; set; }
    public string OwnerId { get; set; }
    public ApplicationUser Owner { get; set; }
    
    public IEnumerable<ApplicationUser> CoOwners { get; set; }
    public ICollection<RulesetAchievement> Achievements { get; set; } = new List<RulesetAchievement>();
    public ICollection<Playthrough> Playthroughs { get; set; } = new List<Playthrough>();
}