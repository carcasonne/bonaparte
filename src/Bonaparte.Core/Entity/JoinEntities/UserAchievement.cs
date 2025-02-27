using Bonaparte.Core.Identity;

namespace Bonaparte.Core.JoinEntities;

public class UserAchievement
{
    public int Id { get; set; }
    
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
    
    public int AchievementId { get; set; }
    public Achievement Achievement { get; set; }
    
    public int PlaythroughId { get; set; }
    public Playthrough Playthrough { get; set; }
    

    public DateTime CompletedDate { get; set; }
    public string Notes { get; set; }
}