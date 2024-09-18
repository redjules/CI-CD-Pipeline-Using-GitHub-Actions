using GitHubActionsDemo.Persistance.Models;
using GitHubActionsDemo.Service.Models;

namespace GitHubActionsDemo.Service.Mappers;

public static class BookMapper
{
    public static NewBookDb Map(this NewBook book)
    {
        return new NewBookDb(
            book.Title,
            book.AuthorId,
            book.Isbn,
            book.DatePublished,
            DateTime.UtcNow,
            DateTime.UtcNow
        );
    }

    public static Book Map(this BookDb book)
    {
        return new Book(
            book.BookId,
            book.Title,
            book.Author.Map(),
            book.Isbn,
            book.DatePublished,
            book.DateCreated,
            book.DateModified
        );
    }
}