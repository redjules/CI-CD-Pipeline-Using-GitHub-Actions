namespace GitHubActionsDemo.Service.Models;

public class NewAuthor
{
    public NewAuthor(
        string firstName,
        string lastName
    )
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public string FirstName { get; }
    public string LastName { get; }
}
