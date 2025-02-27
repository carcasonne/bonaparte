using Bonaparte.Core.JoinEntities;
using Microsoft.AspNetCore.Identity;

namespace Bonaparte.Core;

public class Achievement
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    public int Points { get; set; } 
    
    public string OwnerId { get; set; }
    public IdentityUser Owner { get; set; }
    
    public ICollection<RulesetAchievement> Rulesets { get; set; } = new List<RulesetAchievement>();
    public ICollection<UserAchievement> CompletedBy { get; set; } = new List<UserAchievement>();
}