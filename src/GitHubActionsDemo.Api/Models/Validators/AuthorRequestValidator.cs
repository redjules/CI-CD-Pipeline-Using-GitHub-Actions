using FluentValidation;
using GitHubActionsDemo.Api.Sdk.Authors;
namespace GitHubActionsDemo.Api.Models.Validators;

public class AuthorRequestValidator : AbstractValidator<AuthorRequest>
{
    public AuthorRequestValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
    }
}