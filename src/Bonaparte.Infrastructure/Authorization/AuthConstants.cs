namespace Bonaparte.Infrastructure.Authorization;

public static class AuthConstants
{
    // Roles
    public static class Roles
    {
        public const string Admin = "Admin";
        public const string GameCreator = "GameCreator";
        public const string RulesetCreator = "RulesetCreator";
        public const string PlaythroughCreator = "PlaythroughCreator";
    }
    
    // Policies
    public static class Policies
    {
        public const string RequireAdmin = "RequireAdmin";
        public const string CanCreateGames = "CanCreateGames";
        public const string CanCreateRulesets = "CanCreateRulesets";
        public const string CanCreatePlaythroughs = "CanCreatePlaythroughs";
    }
}