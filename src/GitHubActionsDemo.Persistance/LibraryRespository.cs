using System;
using Dapper;
using GitHubActionsDemo.Persistance.Models;

namespace GitHubActionsDemo.Persistance;

public class LibraryRespository : ILibraryRespository
{
    private readonly IDbContext _dbContext;
    public LibraryRespository(IDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<int> AddAuthorAsync(NewAuthorDb author)
    {
        var sql = @$"INSERT INTO authors (first_name, last_name, date_created, date_modified)
                     VALUES(@FirstName, @LastName, @DateCreated, @DateModified);
                     SELECT LAST_INSERT_ID();";

        using var connection = _dbContext.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<int>(sql, author);
    }

    public async Task<AuthorDb> GetAuthorAsync(int authorId)
    {
        var sql = @$"SELECT
                        a.author_id AS {nameof(AuthorDb.AuthorId)},
                        a.first_name AS {nameof(AuthorDb.FirstName)},
                        a.last_name AS {nameof(AuthorDb.LastName)},
                        a.date_created AS Author{nameof(AuthorDb.DateCreated)},
                        a.date_modified AS Author{nameof(AuthorDb.DateModified)}
                    FROM authors a
                    WHERE a.author_id = @AuthorId;";

        var param = new
        {
            AuthorId = authorId
        };

        using var connection = _dbContext.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<AuthorDb>(sql, param);
    }

    public async Task<IEnumerable<AuthorDb>> GetAuthorsAsync(int page, int pageSize)
    {
        var sql = @$"SELECT
                        a.author_id AS {nameof(AuthorDb.AuthorId)},
                        a.first_name AS {nameof(AuthorDb.FirstName)},
                        a.last_name AS {nameof(AuthorDb.LastName)},
                        a.date_created AS Author{nameof(AuthorDb.DateCreated)},
                        a.date_modified AS Author{nameof(AuthorDb.DateModified)}
                    FROM authors a
                    ORDER BY author_id
                    LIMIT {pageSize}
                    OFFSET {pageSize * (page - 1)};";

        using var connection = _dbContext.CreateConnection();
        return await connection.QueryAsync<AuthorDb>(sql);
    }

    public async Task<int> AddBookAsync(NewBookDb book)
    {
        var sql = @$"INSERT INTO books (title, author_id, isbn, date_published, date_created, date_modified)
                     VALUES(@Title, @AuthorId, @Isbn, @DatePublished, @DateCreated, @DateModified);
                     SELECT LAST_INSERT_ID();";

        using var connection = _dbContext.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<int>(sql, book);
    }

    public async Task<BookDb> GetBookAsync(int bookId)
    {
        var sql = @$"SELECT
                        b.book_id AS {nameof(BookDb.BookId)},
                        b.title AS {nameof(BookDb.Title)},
                        b.isbn AS {nameof(BookDb.Isbn)},
                        b.date_published AS {nameof(BookDb.DatePublished)},
                        b.date_created AS Book{nameof(BookDb.DateCreated)},
                        b.date_modified AS Book{nameof(BookDb.DateModified)},
                        a.author_id AS {nameof(AuthorDb.AuthorId)},
                        a.first_name AS {nameof(AuthorDb.FirstName)},
                        a.last_name AS {nameof(AuthorDb.LastName)},
                        a.date_created AS Author{nameof(AuthorDb.DateCreated)},
                        a.date_modified AS Author{nameof(AuthorDb.DateModified)}
                    FROM books b
                    INNER JOIN authors a ON a.author_id = b.author_id
                    WHERE b.book_id = @BookId;";

        var param = new
        {
            BookId = bookId
        };

        using var connection = _dbContext.CreateConnection();
        var books = await connection.QueryAsync<BookDb, AuthorDb, BookDb>(sql, (book, author) =>
        {
            book.Author = author;
            return book;
        }, param, splitOn: nameof(AuthorDb.AuthorId));

        return books?.FirstOrDefault();
    }

    public async Task<IEnumerable<BookDb>> GetBooksAsync(int page, int pageSize)
    {
        var sql = @$"SELECT
                        b.book_id AS {nameof(BookDb.BookId)},
                        b.title AS {nameof(BookDb.Title)},
                        b.isbn AS {nameof(BookDb.Isbn)},
                        b.date_published AS {nameof(BookDb.DatePublished)},
                        b.date_created AS Book{nameof(BookDb.DateCreated)},
                        b.date_modified AS Book{nameof(BookDb.DateModified)},
                        a.author_id AS {nameof(AuthorDb.AuthorId)},
                        a.first_name AS {nameof(AuthorDb.FirstName)},
                        a.last_name AS {nameof(AuthorDb.LastName)},
                        a.date_created AS Author{nameof(AuthorDb.DateCreated)},
                        a.date_modified AS Author{nameof(AuthorDb.DateModified)}
                    FROM books b
                    INNER JOIN authors a ON a.author_id = b.author_id
                    ORDER BY b.book_id
                    LIMIT {pageSize}
                    OFFSET {pageSize * (page - 1)};";

        using var connection = _dbContext.CreateConnection();

        return await connection.QueryAsync<BookDb, AuthorDb, BookDb>(sql, (book, author) =>
        {
            book.Author = author;
            return book;
        }, splitOn: nameof(AuthorDb.AuthorId));
    }
}
