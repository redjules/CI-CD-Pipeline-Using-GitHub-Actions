using System.Collections;
using System.Net;
using GitHubActionsDemo.Api.Sdk.Shared;
using Microsoft.AspNetCore.Mvc;

namespace GitHubActionsDemo.Api.Controllers;

public class BaseController : ControllerBase
{
    public IResult InternalError()
    {
        return Results.StatusCode((int)HttpStatusCode.InternalServerError);
    }

    public IResult PagedResult<T>(int page, int pageSize, IList<T> result) where T : class
    {
        return Results.Ok(new PagedResponse<T>
        {
            Page = page,
            PageSize = pageSize,
            Result = result
        });
    }
}