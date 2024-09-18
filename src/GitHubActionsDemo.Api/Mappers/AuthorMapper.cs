using GitHubActionsDemo.Api.Sdk.Authors;
using GitHubActionsDemo.Api.Models;
using GitHubActionsDemo.Service.Models;

namespace GitHubActionsDemo.Api.Mappers;

public static class AuthorMapper
{
    public static NewAuthor Map(this AuthorRequest request)
    {
        return new NewAuthor(
            request.FirstName,
            request.LastName
        );
    }

    public static AuthorResponse Map(this Author author)
    {
        return new AuthorResponse
        {
            AuthorId = author.AuthorId,
            FirstName = author.FirstName,
            LastName = author.LastName,
            DateCreated = author.DateCreated,
            DateModified = author.DateModified
        };

    }
}