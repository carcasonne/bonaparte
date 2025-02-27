using Bonaparte.Core.Common;
using Bonaparte.Core.Identity;

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
    public IEnumerable<Achievement> Achievements { get; set; }
}