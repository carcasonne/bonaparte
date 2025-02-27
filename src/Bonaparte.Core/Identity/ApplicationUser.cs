using Bonaparte.Core.JoinEntities;
using Microsoft.AspNetCore.Identity;

namespace Bonaparte.Core.Identity;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public bool Admin { get; set; }
    public ICollection<RulesetCoOwner> CoOwnedRulesets { get; set; } = new List<RulesetCoOwner>();
}