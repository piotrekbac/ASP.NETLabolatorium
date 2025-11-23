using Microsoft.EntityFrameworkCore;

namespace Lab0.Models;

public class BookDbService(AddDbContext context, ILogger<BookDbService> logger) : IBookService
{
    public List<Book> GetBooks()
    {
        return context.Books.ToList();
    }

    public void AddBook(Book book)
    {
        context.Books.Add(book);
        context.SaveChanges();
    }

    public bool UpdateBook(Book book)
    {
        try
        {
            context.Books.Update(book);
            context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException e)
        {
            logger.LogError(e.Message);
            return false; 
        }
        return  true;
    }

    public bool DeleteBook(int Id)
    {
        var deleted = context.Books.Find(Id);
        if (deleted == null)
        {
            return false;
        }
        context.Books.Remove(deleted);
        context.SaveChanges();
        return true;
    }

    public Book? GetBookById(int Id)
    {
        var book =  context.Books.Find(Id);
        return book;
    }
}