namespace GitHubActionsDemo.Persistance;

using GitHubActionsDemo.Persistance.Models;

public interface ILibraryRespository
{
    Task<IEnumerable<BookDb>> GetBooksAsync(int page, int pageSize);
    Task<BookDb> GetBookAsync(int bookId);
    Task<IEnumerable<AuthorDb>> GetAuthorsAsync(int page, int pageSize);
    Task<int> AddBookAsync(NewBookDb book);
    Task<int> AddAuthorAsync(NewAuthorDb author);
    Task<AuthorDb> GetAuthorAsync(int authorId);
}