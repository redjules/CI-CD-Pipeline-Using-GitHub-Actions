
using GitHubActionsDemo.Persistance.Models;
using GitHubActionsDemo.Service.Models;

namespace GitHubActionsDemo.Service.Mappers;

public static class AuthorMapper
{
    public static NewAuthorDb Map(this NewAuthor author)
    {
        return new NewAuthorDb(
            author.FirstName,
            author.LastName,
            DateTime.UtcNow,
            DateTime.UtcNow
        );
    }

    public static Author Map(this AuthorDb author)
    {
        return new Author(
            author.AuthorId,
            author.FirstName,
            author.LastName,
            author.DateCreated,
            author.DateModified
        );
    }
}