using GitHubActionsDemo.Api.Sdk.Authors;
using GitHubActionsDemo.Api.Sdk.Shared;
using GitHubActionsDemo.Api.Mappers;
using GitHubActionsDemo.Service;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace GitHubActionsDemo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorsController : BaseController
{
    private readonly ILibraryService _libraryService;
    private readonly IValidator<AuthorRequest> _authorValidator;
    private readonly IValidator<PageRequest> _pageValidator;

    public AuthorsController(
        ILibraryService libraryService,
        IValidator<AuthorRequest> authorValidator,
        IValidator<PageRequest> pageValidator
    )
    {
        _libraryService = libraryService ?? throw new ArgumentNullException(nameof(libraryService));
        _authorValidator = authorValidator ?? throw new ArgumentNullException(nameof(authorValidator));
        _pageValidator = pageValidator ?? throw new ArgumentNullException(nameof(pageValidator));
    }

    [HttpPost]
    public async Task<IResult> AddAuthorAsync([FromBody] AuthorRequest authorRequest)
    {
        var validationResult = await _authorValidator.ValidateAsync(authorRequest);

        if (!validationResult.IsValid)
            return Results.ValidationProblem(validationResult.ToDictionary());


        var result = await _libraryService.AddAuthorAsync(authorRequest.Map());
        return result.Match(
            success => Results.Ok(success.Value.Map()),
            error => InternalError()
        );
    }

    [HttpGet("{authorId}")]
    public async Task<IResult> GetAuthorAsync(int authorId)
    {
        var result = await _libraryService.GetAuthorAsync(authorId);
        return result.Match(
            success => Results.Ok(success.Value.Map()),
            notfound => Results.NotFound(),
            error => InternalError()
        );
    }

    [HttpGet]
    public async Task<IResult> GetAuthorsAsync([FromQuery] PageRequest parameters)
    {
        var validationResult = await _pageValidator.ValidateAsync(parameters);

        if (!validationResult.IsValid)
            return Results.ValidationProblem(validationResult.ToDictionary());

        var result = await _libraryService.GetAuthorsAsync(parameters.Page, parameters.PageSize);

        return result.Match(
            success => PagedResult(parameters.Page, parameters.PageSize, success.Value.Select(x => x.Map()).ToList()),
            error => InternalError()
        );
    }
}
