using Microsoft.EntityFrameworkCore;

namespace Lab0.Models;

// publiczna klasa - serwis który obsługuje operacje na naszej książce - implementujemy interfejs IBookService 
public class BookDbService : IBookService
{
    // prywatne pole, które przechowuje kontekst bazy danych 
    private readonly AddDbContext _context;

    // konstruktor kontekstu, jest on wstrzykiwany przez Dependency Injection
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

    // pobieramy wszystkie książki z bazy jako listę, dołączamy dane wydawnictwa (Publisher Entity)
    public List<Book> GetBooks()
    {
        return _context.Books.Include(b => b.PublisherEntity).ToList();
    }

    // dodajemy nową książkę do bazy, po dodaniu wywołujemy SaveChanges() co utrwala zmiany w SQLite
    public void AddBook(Book book)
    {
        _context.Books.Add(book);
        _context.SaveChanges();
    }

    // aktualizacja istniejącej ksiązki, zwracamy true jeżeli operacja się powiodła
    public bool UpdateBook(Book book)
    {
        try
        {
            _context.Books.Update(book);
            _context.SaveChanges();
            return true;
        }
        // w przypadku, gdy operacja się nie powiodła zwracamy false
        catch (DbUpdateConcurrencyException)
        {
            return false;
        }
    }

    // usuwamy książkę po jej Id, jeżeli nie istnieje zwracamy false, jeżeli istnieje to usuwamy i zapisujemy zmiany w bazie danych
    public bool DeleteBook(int id)
    {
        var deleted = _context.Books.Find(id);
        if (deleted == null) return false;
        _context.Books.Remove(deleted);
        _context.SaveChanges();
        return true;
    }

    // pobieramy pojedyczną książkę po Id, dołączamy dane wydawnictwa, jeżeli nie istnieje zwracamy null 
    public Book? GetBookById(int id)
    {
        return _context.Books
            .Include(b => b.PublisherEntity)
            .FirstOrDefault(b => b.Id == id);
    }
}