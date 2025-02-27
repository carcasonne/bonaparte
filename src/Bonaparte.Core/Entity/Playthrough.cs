using Bonaparte.Core.Identity;

namespace Bonaparte.Core;

// A playthrough is a session of playing some game with some ruleset 
public class Playthrough
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    
    public int GameId { get; set; }
    public Game Game { get; set; }
    public string OwnerId { get; set; }
    public ApplicationUser Owner { get; set; }
    
    public IEnumerable<ApplicationUser> Players { get; set; }
}