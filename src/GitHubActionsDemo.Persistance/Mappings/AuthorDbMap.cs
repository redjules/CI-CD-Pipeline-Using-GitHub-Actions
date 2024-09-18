using Dapper.FluentMap.Mapping;
using GitHubActionsDemo.Persistance.Models;

internal class AuthorDbMap : EntityMap<AuthorDb>
{
    internal AuthorDbMap()
    {
        Map(u => u.DateCreated).ToColumn($"Author{nameof(AuthorDb.DateCreated)}");
        Map(u => u.DateModified).ToColumn($"Author{nameof(AuthorDb.DateModified)}");
    }
}