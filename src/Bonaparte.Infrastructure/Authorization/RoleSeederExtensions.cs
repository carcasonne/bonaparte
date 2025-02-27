using Microsoft.Extensions.DependencyInjection;

namespace Bonaparte.Infrastructure.Authorization;

public static class RoleSeederExtensions
{
    public static IServiceCollection AddRoleSeeder(this IServiceCollection services)
    {
        services.AddScoped<RoleSeeder>();
        return services;
    }
    
    // Helper method to seed roles at app startup
    public static async Task SeedRolesAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var roleSeeder = scope.ServiceProvider.GetRequiredService<RoleSeeder>();
        await roleSeeder.SeedRolesAndAdminUser();
    }
}