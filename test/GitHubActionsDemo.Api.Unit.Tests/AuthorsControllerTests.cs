using System.Net;
using FluentValidation;
using FluentValidation.Results;
using GitHubActionsDemo.Api.Controllers;
using GitHubActionsDemo.Service;
using GitHubActionsDemo.Service.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using GitHubActionsDemo.Api.Sdk.Authors;
using GitHubActionsDemo.Api.Sdk.Shared;

namespace GitHubActionsDemo.Api.Unit.Tests;

[Trait("Category", "Unit")]
public class AuthorsControllerTests
{
    private readonly Mock<ILibraryService> _libraryService;
    private readonly Mock<IValidator<AuthorRequest>> _authorValidator;
    private readonly Mock<IValidator<PageRequest>> _pageValidator;

    private readonly AuthorsController _sut;

    public AuthorsControllerTests()
    {
        _libraryService = new Mock<ILibraryService>();
        _authorValidator = new Mock<IValidator<AuthorRequest>>();
        _pageValidator = new Mock<IValidator<PageRequest>>();
        _sut = new AuthorsController(_libraryService.Object, _authorValidator.Object, _pageValidator.Object);
    }

    [Fact]
    public async Task Given_invalid_request_should_return_validation_problem()
    {
        var request = new AuthorRequest
        {
            FirstName = "",
            LastName = ""
        };

        var failures = new List<ValidationFailure>
        {
            new ValidationFailure("FirstName", "Missing"),
            new ValidationFailure("LastName", "Missing")
        };

        _authorValidator.Setup(x => x.ValidateAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(failures));

        var result = await _sut.AddAuthorAsync(request);
        result.ShouldBeOfType<ProblemHttpResult>();
        var problem = result as ProblemHttpResult;
        problem.ShouldNotBeNull();
    }

    [Fact]
    public async Task Given_valid_request_should_return_author()
    {
        var request = new AuthorRequest
        {
            FirstName = "Joe",
            LastName = "Bloggs"
        };

        var mockAuthor = new Author(1, request.FirstName, request.LastName, DateTime.UtcNow, DateTime.UtcNow);

        _authorValidator.Setup(x => x.ValidateAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _libraryService.Setup(
            x => x.AddAuthorAsync(It.Is<NewAuthor>(a =>
                                                   a.FirstName == request.FirstName &&
                                                   a.LastName == request.LastName)))
                                                   .ReturnsAsync(new OneOf.Types.Success<Author>(mockAuthor));

        var result = await _sut.AddAuthorAsync(request);
        result.ShouldBeOfType<Ok<AuthorResponse>>();
        var successValue = result as Ok<AuthorResponse>;
        successValue.ShouldNotBeNull();
        successValue.StatusCode.ShouldBe((int)HttpStatusCode.OK);
        successValue.Value.FirstName.ShouldBe(request.FirstName);
        successValue.Value.LastName.ShouldBe(request.LastName);
    }

    // TODO: Note in a production application there would be a complete set of unit tests here.
}