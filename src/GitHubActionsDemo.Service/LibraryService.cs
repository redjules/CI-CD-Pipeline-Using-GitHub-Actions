using GitHubActionsDemo.Persistance;
using GitHubActionsDemo.Service.Mappers;
using GitHubActionsDemo.Service.Models;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;
using NotFound = OneOf.Types.NotFound;

namespace GitHubActionsDemo.Service;

public class LibraryService : ILibraryService
{
    private readonly ILibraryRespository _libraryRepository;
    private readonly ILogger<LibraryService> _logger;

    public LibraryService(
        ILibraryRespository libraryRepository,
        ILogger<LibraryService> logger
    )
    {
        _libraryRepository = libraryRepository ?? throw new ArgumentNullException(nameof(libraryRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<OneOf<Success<Author>, Error>> AddAuthorAsync(NewAuthor newAuthor)
    {
        try
        {
            var authorId = await _libraryRepository.AddAuthorAsync(newAuthor.Map());
            var author = await _libraryRepository.GetAuthorAsync(authorId);
            return new Success<Author>(author.Map());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding author");
            return new Error();
        }
    }

    public async Task<OneOf<Success<Book>, Error>> AddBookAsync(NewBook newBook)
    {
        try
        {
            var bookId = await _libraryRepository.AddBookAsync(newBook.Map());
            var book = await _libraryRepository.GetBookAsync(bookId);
            return new Success<Book>(book.Map());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding book");
            return new Error();
        }
    }

    public async Task<OneOf<Success<Author>, NotFound, Error>> GetAuthorAsync(int authorId)
    {
        try
        {
            var author = await _libraryRepository.GetAuthorAsync(authorId);
            if (author == null)
                return new NotFound();

            return new Success<Author>(author.Map());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting author");
            return new Error();
        }
    }

    public async Task<OneOf<Success<IEnumerable<Author>>, Error>> GetAuthorsAsync(int page, int pageSize)
    {
        try
        {
            var authors = await _libraryRepository.GetAuthorsAsync(page, pageSize);
            if (authors == null)
                return new Success<IEnumerable<Author>>();

            return new Success<IEnumerable<Author>>(authors?.Select(author => author.Map()));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting authors");
            return new Error();
        }
    }

    public async Task<OneOf<Success<Book>, NotFound, Error>> GetBookAsync(int bookId)
    {
        try
        {
            var book = await _libraryRepository.GetBookAsync(bookId);
            if (book == null)
                return new NotFound();

            return new Success<Book>(book.Map());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting book");
            return new Error();
        }
    }

    public async Task<OneOf<Success<IEnumerable<Book>>, Error>> GetBooksAsync(int page, int pageSize)
    {
        try
        {
            var books = await _libraryRepository.GetBooksAsync(page, pageSize);
            if (books == null)
                return new Success<IEnumerable<Book>>();

            return new Success<IEnumerable<Book>>(books?.Select(book => book.Map()));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting books");
            return new Error();
        }
    }
}