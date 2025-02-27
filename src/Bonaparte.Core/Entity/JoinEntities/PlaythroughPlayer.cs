using Bonaparte.Core.Identity;

namespace Bonaparte.Core.JoinEntities;

public class PlaythroughPlayer
{
    public int PlaythroughId { get; set; }
    public Playthrough Playthrough { get; set; }
    
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
}