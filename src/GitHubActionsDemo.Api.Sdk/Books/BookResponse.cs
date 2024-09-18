using GitHubActionsDemo.Api.Sdk.Authors;

namespace GitHubActionsDemo.Api.Sdk.Books;

public class BookResponse
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public AuthorResponse Author { get; set; }
    public string Isbn { get; set; }
    public DateTime DatePublished { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
}
