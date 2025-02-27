using Microsoft.Extensions.DependencyInjection;

namespace Bonaparte.Infrastructure.Authorization;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddBonaparteAuthorization(this IServiceCollection services)
    {
        services.AddAuthorizationCore(options =>
        {
            // Admin policy - only admins
            options.AddPolicy(AuthConstants.Policies.RequireAdmin, policy =>
                policy.RequireRole(AuthConstants.Roles.Admin));

            // Game creation policy - admins and game creators
            options.AddPolicy(AuthConstants.Policies.CanCreateGames, policy =>
                policy.RequireAssertion(context =>
                    context.User.IsInRole(AuthConstants.Roles.Admin) ||
                    context.User.IsInRole(AuthConstants.Roles.GameCreator)));

            // Ruleset creation policy - admins and ruleset creators
            options.AddPolicy(AuthConstants.Policies.CanCreateRulesets, policy =>
                policy.RequireAssertion(context =>
                    context.User.IsInRole(AuthConstants.Roles.Admin) ||
                    context.User.IsInRole(AuthConstants.Roles.RulesetCreator)));

            // Playthrough creation policy - admins and playthrough creators
            options.AddPolicy(AuthConstants.Policies.CanCreatePlaythroughs, policy =>
                policy.RequireAssertion(context =>
                    context.User.IsInRole(AuthConstants.Roles.Admin) ||
                    context.User.IsInRole(AuthConstants.Roles.PlaythroughCreator)));
        });
        
        return services;
    }
}