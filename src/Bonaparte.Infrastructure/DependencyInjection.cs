using Bonaparte.Core.Interfaces.Repository;
using Bonaparte.Infrastructure.Authorization;
using Bonaparte.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Bonaparte.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Register repositories
        services.AddScoped<IGameRepository, GameRepository>();
        // Register other repositories here...
        
        // Add authorization
        services.AddBonaparteAuthorization();
        services.AddRoleSeeder();
        
        // Add HttpContextAccessor for user claims access
        services.AddHttpContextAccessor();
        
        return services;
    }
}