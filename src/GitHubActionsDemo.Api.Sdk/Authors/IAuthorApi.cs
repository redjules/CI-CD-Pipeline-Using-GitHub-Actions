using GitHubActionsDemo.Api.Sdk.Shared;
using Refit;

namespace GitHubActionsDemo.Api.Sdk.Authors;

public interface IAuthorApi
{
    [Get("/authors/")]
    Task<PagedResponse<AuthorResponse>> GetAuthorsAsync();

    [Get("/authors/{authorId}")]
    Task<AuthorResponse> GetAuthorAsync(int authorId);

    [Post("/authors/")]
    Task<AuthorResponse> CreateAuthorAsync([Body] AuthorRequest request);
}
