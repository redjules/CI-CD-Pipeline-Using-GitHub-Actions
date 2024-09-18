using Dapper.FluentMap;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GitHubActionsDemo.Persistance.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddPersistanceDependencies(this IServiceCollection services)
    {
        return services.AddSingleton<IDbContext, DbContext>()
                       .AddScoped<ILibraryRespository, LibraryRespository>();
    }

    public static async Task InitDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IDbContext>();
        await context.Init();

        FluentMapper.Initialize(config =>
        {
            config.AddMap(new AuthorDbMap());
            config.AddMap(new BookDbMap());
        });
    }
}
