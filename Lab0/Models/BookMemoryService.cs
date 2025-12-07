namespace Lab0.Models;

public class BookMemoryService : IBookService
{
    private static Dictionary<int, Book> _books = new()
    {
        {1, new Book() {Id = 1, Author = "Maacieeek", Title = "Zakochany kundel"}}
    };

    private int i = 2;

    public Task<PagingListAsync<Book>> GetBooksPage(int page, int size)
    {
        throw new NotImplementedException();
    }

    public List<Book> GetBooks()
    {
        return _books.Values.ToList();
    }

    public void AddBook(Book book)
    {
         _books.Add(++i, book);
    }

    public bool UpdateBook(Book book)
    {
        if (_books.ContainsKey(book.Id))
        {
            _books[book.Id] = book;
            return true;
        }
        // false gdy nie ma czego aktualizaować
        return false;
    }

    public bool DeleteBook(int Id)
    {
        if (_books.ContainsKey(Id))
        {
            _books.Remove(Id);
            return true;
        }
        // false gdy nie ma czego usuwać
        return false;
    }

    public Book? GetBookById(int Id)
    {
        if (_books.ContainsKey(Id))
        {
            return _books[Id];
        }
        // gdy nie ma obiektu to zwracamy null
        return null;
    }
}