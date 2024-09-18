using GitHubActionsDemo.Persistance;
using GitHubActionsDemo.Persistance.Models;
using GitHubActionsDemo.Service.Models;
using Microsoft.Extensions.Logging;

namespace GitHubActionsDemo.Service.Unit.Tests;

[Trait("Category", "Unit")]
public class LibraryServiceTests
{
    private readonly Mock<ILibraryRespository> _libraryRespository;
    private readonly Mock<ILogger<LibraryService>> _logger;
    private readonly LibraryService _sut;

    public LibraryServiceTests()
    {
        _libraryRespository = new Mock<ILibraryRespository>();
        _logger = new Mock<ILogger<LibraryService>>();
        _sut = new LibraryService(_libraryRespository.Object, _logger.Object);
    }

    [Fact]
    public async Task Given_new_author_should_return_author()
    {
        var newAuthor = new NewAuthor("Joe", "Bloggs");

        var mockAuthorDb = new AuthorDb
        {
            AuthorId = 1,
            FirstName = newAuthor.FirstName,
            LastName = newAuthor.LastName,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        _libraryRespository.Setup(x => x.AddAuthorAsync(It.IsAny<NewAuthorDb>())).ReturnsAsync(mockAuthorDb.AuthorId);
        _libraryRespository.Setup(x => x.GetAuthorAsync(1)).ReturnsAsync(mockAuthorDb);

        var result = await _sut.AddAuthorAsync(newAuthor);
        result.IsT0.ShouldBeTrue();
        result.AsT0.Value.FirstName.ShouldBe(mockAuthorDb.FirstName);
        result.AsT0.Value.LastName.ShouldBe(mockAuthorDb.LastName);
    }

    [Fact]
    public async Task Given_new_book_should_return_book()
    {
        var newBook = new NewBook("Once Upon A Time", 1, "1234", DateTime.UtcNow);

        var mockBookDb = new BookDb
        {
            BookId = 1,
            Title = newBook.Title,
            Author = new AuthorDb { AuthorId = 1, FirstName = "Joe", LastName = "Bloggs" },
            Isbn = newBook.Isbn,
            DatePublished = newBook.DatePublished,
            DateCreated = DateTime.UtcNow,
            DateModified = DateTime.UtcNow
        };

        _libraryRespository.Setup(x => x.AddBookAsync(It.IsAny<NewBookDb>())).ReturnsAsync(mockBookDb.BookId);
        _libraryRespository.Setup(x => x.GetBookAsync(1)).ReturnsAsync(mockBookDb);

        var result = await _sut.AddBookAsync(newBook);
        result.IsT0.ShouldBeTrue();
        result.AsT0.Value.Title.ShouldBe(newBook.Title);
        result.AsT0.Value.Isbn.ShouldBe(newBook.Isbn);
    }

    // TODO: Note in a production application there would be a complete set of unit tests here.
}