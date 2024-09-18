namespace GitHubActionsDemo.Persistance.Models;

public class NewBookDb
{
    public NewBookDb(
        string title,
        int authorId,
        string isbn,
        DateTime datePublished,
        DateTime dateCreated,
        DateTime dateModified
    )
    {
        Title = title;
        AuthorId = authorId;
        Isbn = isbn;
        DatePublished = datePublished;
        DateCreated = dateCreated;
        DateModified = dateModified;
    }

    public int BookId { get; }
    public string Title { get; }
    public int AuthorId { get; }
    public string Isbn { get; }
    public DateTime DatePublished { get; }
    public DateTime DateCreated { get; }
    public DateTime DateModified { get; }
}