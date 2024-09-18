namespace GitHubActionsDemo.Service.Models;

public class Author
{
    public Author(
        int authorId,
        string firstName,
        string lastName,
        DateTime dateCreated,
        DateTime dateUpdated
    )
    {
        AuthorId = authorId;
        FirstName = firstName;
        LastName = lastName;
        DateCreated = dateCreated;
        DateModified = dateUpdated;
    }

    public int AuthorId { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public DateTime DateCreated { get; }
    public DateTime DateModified { get; }
}
