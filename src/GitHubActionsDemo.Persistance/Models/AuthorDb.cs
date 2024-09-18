namespace GitHubActionsDemo.Persistance.Models;

public class AuthorDb
{
    public int AuthorId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
}
