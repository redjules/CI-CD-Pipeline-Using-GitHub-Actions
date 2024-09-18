using GitHubActionsDemo.Api.Sdk.Books;
using GitHubActionsDemo.Api.Sdk.Shared;
using GitHubActionsDemo.Api.Mappers;
using GitHubActionsDemo.Service;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace GitHubActionsDemo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : BaseController
{
    private readonly ILibraryService _libraryService;
    private readonly IValidator<BookRequest> _bookValidator;
    private readonly IValidator<PageRequest> _pageValidator;

    public BooksController(
        ILibraryService libraryService,
        IValidator<BookRequest> bookValidator,
        IValidator<PageRequest> pageValidator
    )
    {
        _libraryService = libraryService ?? throw new ArgumentNullException(nameof(libraryService));
        _bookValidator = bookValidator ?? throw new ArgumentNullException(nameof(bookValidator));
        _pageValidator = pageValidator ?? throw new ArgumentNullException(nameof(pageValidator));
    }

    [HttpGet]
    public async Task<IResult> GetBooksAsync([FromQuery] PageRequest parameters)
    {
        var validationResult = await _pageValidator.ValidateAsync(parameters);

        if (!validationResult.IsValid)
            return Results.ValidationProblem(validationResult.ToDictionary());

        var result = await _libraryService.GetBooksAsync(parameters.Page, parameters.PageSize);

        return result.Match(
            success => PagedResult(parameters.Page, parameters.PageSize, success.Value.Select(x => x.Map()).ToList()),
            error => InternalError()
        );
    }

    [HttpGet("{bookId}")]
    public async Task<IResult> GetBookAsync(int bookId)
    {
        var result = await _libraryService.GetBookAsync(bookId);
        return result.Match(
            success => Results.Ok(success.Value.Map()),
            notfound => Results.NotFound(),
            error => InternalError()
        );
    }

    [HttpPost]
    public async Task<IResult> AddBookAsync([FromBody] BookRequest bookRequest)
    {
        var validationResult = await _bookValidator.ValidateAsync(bookRequest);

        if (!validationResult.IsValid)
            return Results.ValidationProblem(validationResult.ToDictionary());

        var result = await _libraryService.AddBookAsync(bookRequest.Map());
        return result.Match(
            success => Results.Ok(success.Value.Map()),
            error => InternalError()
        );
    }
}
