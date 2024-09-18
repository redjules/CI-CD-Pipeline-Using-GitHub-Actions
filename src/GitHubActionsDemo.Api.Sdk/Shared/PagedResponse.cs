using System.Collections;
namespace GitHubActionsDemo.Api.Sdk.Shared;

public class PagedResponse<T> where T : class
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int Count
    {
        get
        {
            return Result.Count;
        }
    }
    public IList<T> Result { get; set; }
}