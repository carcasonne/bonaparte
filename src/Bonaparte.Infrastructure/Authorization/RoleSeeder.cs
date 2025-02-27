using Bonaparte.Core.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Bonaparte.Infrastructure.Authorization;

public class RoleSeeder
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<RoleSeeder> _logger;
    
    public RoleSeeder(IServiceProvider serviceProvider, ILogger<RoleSeeder> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task SeedRolesAndAdminUser()
    {
        using var scope = _serviceProvider.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        // First create all roles
        await SeedRoles(roleManager);
        
        // Then create the admin user and assign roles
        await SeedAdminUser(userManager, roleManager);
    }
    
    private async Task SeedRoles(RoleManager<IdentityRole> roleManager)
    {
        // Define all roles to seed
        var roles = new[]
        {
            AuthConstants.Roles.Admin,
            AuthConstants.Roles.GameCreator,
            AuthConstants.Roles.RulesetCreator,
            AuthConstants.Roles.PlaythroughCreator
        };

        // Create roles if they don't exist
        foreach (var roleName in roles)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(roleName));
                if (result.Succeeded)
                {
                    _logger.LogInformation("Created role {Role}", roleName);
                }
                else
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    _logger.LogError("Failed to create role {Role}: {Errors}", roleName, errors);
                }
            }
        }
    }
    
    private async Task SeedAdminUser(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Create admin user if it doesn't exist
        var adminEmail = "admin@bonaparte.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
            };

            var password = "Admin123!"; // Consider retrieving from configuration in production
            var result = await userManager.CreateAsync(adminUser, password);

            if (result.Succeeded)
            {
                _logger.LogInformation("Created admin user {Email}", adminEmail);

                // Add admin to Admin role
                await userManager.AddToRoleAsync(adminUser, AuthConstants.Roles.Admin);
                _logger.LogInformation("Added admin user to Admin role");
            }
            else
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogError("Failed to create admin user: {Errors}", errors);
            }
        }
    }
}