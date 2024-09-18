using FluentValidation;
using GitHubActionsDemo.Api.Sdk.Books;
namespace GitHubActionsDemo.Api.Models.Validators;

public class BookRequestValidator : AbstractValidator<BookRequest>
{
    public BookRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.AuthorId).NotEmpty();

        RuleFor(x => x.Isbn)
            .NotEmpty()
            .Matches(@"^(?=(?:\D*\d){10}(?:(?:\D*\d){3})?$)[\d-]+$");

        RuleFor(x => x.DatePublished)
            .NotEmpty();
    }
}