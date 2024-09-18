using GitHubActionsDemo.Api.Sdk.Books;
using GitHubActionsDemo.Api.Models;
using GitHubActionsDemo.Service.Models;

namespace GitHubActionsDemo.Api.Mappers;

public static class BookMapper
{
    public static NewBook Map(this BookRequest request)
    {
        return new NewBook(
            request.Title,
            request.AuthorId,
            request.Isbn,
            request.DatePublished
        );
    }

    public static BookResponse Map(this Book book)
    {
        return new BookResponse
        {
            BookId = book.BookId,
            Title = book.Title,
            Author = book.Author.Map(),
            Isbn = book.Isbn,
            DatePublished = book.DatePublished,
            DateCreated = book.DateCreated,
            DateModified = book.DateModified
        };
    }
}