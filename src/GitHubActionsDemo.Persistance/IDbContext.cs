using System.Data;

namespace GitHubActionsDemo.Persistance;

public interface IDbContext
{
    IDbConnection CreateConnection();
    Task Init();
}