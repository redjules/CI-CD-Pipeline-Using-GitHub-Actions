using GitHubActionsDemo.Api.Sdk.Shared;
using Refit;

namespace GitHubActionsDemo.Api.Sdk.Books;

public interface IBookApi
{
    [Get("/books/")]
    Task<PagedResponse<BookResponse>> GetBooksAsync();

    [Get("/books/{bookId}")]
    Task<BookResponse> GetBookAsync(int bookId);

    [Post("/books/")]
    Task<BookResponse> CreateBookAsync([Body] BookRequest request);
}
