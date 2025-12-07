using Microsoft.EntityFrameworkCore;

namespace Lab0.Models;

public class BookDbService : IBookService
{
    private readonly AddDbContext _context;

    public BookDbService(AddDbContext context)
    {
        _context = context;
    }
    
    // Metoda asynchroniczna, pobierająca stronę danych - zwraca specjalny obiekt PagingList który obsługuje metdane stronicowania
    public async Task<PagingListAsync<Book>> GetBooksPage(int page, int size)
    {
        return await PagingListAsync<Book>.CreateAsync(
            _context.Books
                .Include(b => b.PublisherEntity)
                // pamiętam o sortowaniu podczas stronicowania
                .OrderBy(b => b.Title),
            page,
            size);
    }

    public List<Book> GetBooks()
    {
        return _context.Books.Include(b => b.PublisherEntity).ToList();
    }

    public void AddBook(Book book)
    {
        _context.Books.Add(book);
        _context.SaveChanges();
    }

    public bool UpdateBook(Book book)
    {
        try
        {
            _context.Books.Update(book);
            _context.SaveChanges();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            return false;
        }
    }

    public bool DeleteBook(int id)
    {
        var deleted = _context.Books.Find(id);
        if (deleted == null) return false;
        _context.Books.Remove(deleted);
        _context.SaveChanges();
        return true;
    }

    public Book? GetBookById(int id)
    {
        return _context.Books
            .Include(b => b.PublisherEntity)
            .FirstOrDefault(b => b.Id == id);
    }
}