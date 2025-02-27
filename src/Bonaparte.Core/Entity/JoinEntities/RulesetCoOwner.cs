using Bonaparte.Core.Identity;

namespace Bonaparte.Core.JoinEntities;

public class RulesetCoOwner
{
    public int RulesetId { get; set; }
    public Ruleset Ruleset { get; set; }
    
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
}