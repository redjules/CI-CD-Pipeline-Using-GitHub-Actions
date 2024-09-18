using Dapper.FluentMap.Mapping;
using GitHubActionsDemo.Persistance.Models;

internal class BookDbMap : EntityMap<BookDb>
{
    internal BookDbMap()
    {
        Map(u => u.DateCreated).ToColumn($"Book{nameof(BookDb.DateCreated)}");
        Map(u => u.DateModified).ToColumn($"Book{nameof(BookDb.DateModified)}");
    }
}