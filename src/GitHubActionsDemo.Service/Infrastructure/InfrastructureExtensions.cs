using GitHubActionsDemo.Persistance.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace GitHubActionsDemo.Service.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            return services
                .AddPersistanceDependencies()
                .AddScoped<ILibraryService, LibraryService>();
        }
    }
}