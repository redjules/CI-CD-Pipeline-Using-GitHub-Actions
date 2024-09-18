namespace GitHubActionsDemo.Persistance.Models;

public class BookDb
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public AuthorDb Author { get; set; }
    public string Isbn { get; set; }
    public DateTime DatePublished { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
}
