namespace Bonaparte.Core.JoinEntities;

public class RulesetAchievement
{
    public int RulesetId { get; set; }
    public Ruleset Ruleset { get; set; }
    
    public int AchievementId { get; set; }
    public Achievement Achievement { get; set; }
}