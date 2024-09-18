namespace GitHubActionsDemo.Service.Models;

public class NewBook
{
    public NewBook(
        string title,
        int authorId,
        string isbn,
        DateTime datePublished
    )
    {
        Title = title;
        AuthorId = authorId;
        Isbn = isbn;
        DatePublished = datePublished;
    }

    public string Title { get; }
    public int AuthorId { get; }
    public string Isbn { get; }
    public DateTime DatePublished { get; }
}