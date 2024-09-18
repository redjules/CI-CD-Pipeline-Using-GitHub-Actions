using GitHubActionsDemo.Api.Sdk.Authors;
using GitHubActionsDemo.Api.Sdk.Books;
using Microsoft.Extensions.Configuration;
using Refit;
using Shouldly;
using Xunit.Sdk;

namespace GitHubActionsDemo.Api.Integration.Tests;

[Trait("Category", "Integration")]
public class IntegrationTests
{
    public readonly IConfiguration _config;
    private readonly IAuthorApi _authorApi;
    private readonly IBookApi _bookApi;

    public IntegrationTests(IConfiguration config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
        var baseUrl = _config.GetValue<string>("BASE_URL");
        _authorApi = RestService.For<IAuthorApi>(baseUrl);
        _bookApi = RestService.For<IBookApi>(baseUrl);
    }

    [Fact]
    public async Task Given_valid_author_should_create_author()
    {
        var request = new AuthorRequest
        {
            FirstName = $"{Guid.NewGuid()}",
            LastName = $"{Guid.NewGuid()}"
        };

        var author = await _authorApi.CreateAuthorAsync(request);
        author.AuthorId.ShouldNotBe(0);
        author.FirstName.ShouldBe(request.FirstName);
        author.LastName.ShouldBe(request.LastName);
        author.DateCreated.ShouldBeInRange(DateTime.UtcNow.AddSeconds(-5), DateTime.UtcNow.AddSeconds(5));
        author.DateModified.ShouldBeInRange(DateTime.UtcNow.AddSeconds(-5), DateTime.UtcNow.AddSeconds(5));
    }

    [Fact]
    public async Task Given_created_author_should_return_author_on_get()
    {
        var request = new AuthorRequest
        {
            FirstName = $"{Guid.NewGuid()}",
            LastName = $"{Guid.NewGuid()}"
        };

        var createdAuthor = await _authorApi.CreateAuthorAsync(request);

        var author = await _authorApi.GetAuthorAsync(createdAuthor.AuthorId);
        author.AuthorId.ShouldBe(createdAuthor.AuthorId);
        author.FirstName.ShouldBe(request.FirstName);
        author.LastName.ShouldBe(request.LastName);
        author.DateCreated.ShouldBeInRange(DateTime.UtcNow.AddSeconds(-5), DateTime.UtcNow.AddSeconds(5));
        author.DateModified.ShouldBeInRange(DateTime.UtcNow.AddSeconds(-5), DateTime.UtcNow.AddSeconds(5));
    }


    [Fact]
    public async Task Given_valid_book_should_return_book()
    {
        var authorRequest = new AuthorRequest
        {
            FirstName = $"{Guid.NewGuid()}",
            LastName = $"{Guid.NewGuid()}"
        };

        var createdAuthor = await _authorApi.CreateAuthorAsync(authorRequest);

        var bookRequest = new BookRequest
        {
            AuthorId = createdAuthor.AuthorId,
            Title = "Once upon a time",
            Isbn = "978-93-5489-3896",
            DatePublished = new DateTime(2020, 01, 01)
        };

        var createdBook = await _bookApi.CreateBookAsync(bookRequest);
        createdBook.Title.ShouldBe(bookRequest.Title);
        createdBook.Isbn.ShouldBe(bookRequest.Isbn);
        createdBook.Author.AuthorId.ShouldBe(createdAuthor.AuthorId);
        createdBook.Author.FirstName.ShouldBe(authorRequest.FirstName);
        createdBook.Author.LastName.ShouldBe(authorRequest.LastName);
    }
}