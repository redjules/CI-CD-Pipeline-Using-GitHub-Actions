using GitHubActionsDemo.Service.Models;
using OneOf;
using OneOf.Types;
using NotFound = OneOf.Types.NotFound;

namespace GitHubActionsDemo.Service;

public interface ILibraryService
{
    Task<OneOf<Success<IEnumerable<Book>>, Error>> GetBooksAsync(int page, int pageSize);
    Task<OneOf<Success<Book>, NotFound, Error>> GetBookAsync(int bookId);
    Task<OneOf<Success<Book>, Error>> AddBookAsync(NewBook newBook);
    Task<OneOf<Success<Author>, Error>> AddAuthorAsync(NewAuthor newAuthor);
    Task<OneOf<Success<Author>, NotFound, Error>> GetAuthorAsync(int authorId);
    Task<OneOf<Success<IEnumerable<Author>>, Error>> GetAuthorsAsync(int page, int pageSize);
}