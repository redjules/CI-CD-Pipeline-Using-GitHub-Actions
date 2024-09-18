using FluentValidation;
using GitHubActionsDemo.Api.Sdk.Shared;
namespace GitHubActionsDemo.Api.Models.Validators;

public class PageParametersValidator : AbstractValidator<PageRequest>
{
    public PageParametersValidator()
    {
        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
    }
}