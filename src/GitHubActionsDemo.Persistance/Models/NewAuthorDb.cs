namespace GitHubActionsDemo.Persistance.Models;

public class NewAuthorDb
{
    public NewAuthorDb(
        string firstName,
        string lastName,
        DateTime dateCreated,
        DateTime dateModified
    )
    {
        FirstName = firstName;
        LastName = lastName;
        DateCreated = dateCreated;
        DateModified = dateModified;
    }

    public string FirstName { get; }
    public string LastName { get; }
    public DateTime DateCreated { get; }
    public DateTime DateModified { get; }
}
