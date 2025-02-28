using Bonaparte.Core.Common;
using Bonaparte.Core.Identity;
using Bonaparte.Core.JoinEntities;

namespace Bonaparte.Core;

public class Achievement : AuditableEntity
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    public int Points { get; set; } 
    
    public string OwnerId { get; set; }
    public ApplicationUser Owner { get; set; }
    
    public ICollection<RulesetAchievement> Rulesets { get; set; } = new List<RulesetAchievement>();
    public ICollection<UserAchievement> CompletedBy { get; set; } = new List<UserAchievement>();
}