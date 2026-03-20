using UserManagement.Infrastructure.Helpers;
using UserManagement.Infrastructure.Repositories;
using UserManagement.Domain.Interfaces;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddUserManagementInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<DataContext>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();

        return services;
    }
}