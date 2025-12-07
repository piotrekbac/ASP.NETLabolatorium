namespace Lab0.Models;

// Prosta implementacja IBookService, która trzyma dane w pamięci
public class BookMemoryService : IBookService
{
    // "pseudo-baza" – słownik z przykładową książką
    private static Dictionary<int, Book> _books = new()
    {
        {1, new Book() {Id = 1, Author = "Maacieeek", Title = "Zakochany kundel"}}
    };

    // licznik do generowania kolejnych Id
    private int i = 2;

    // metoda stronicowania jeszcze niezaimplementowana – rzuca wyjątek
    public Task<PagingListAsync<Book>> GetBooksPage(int page, int size)
    {
        throw new NotImplementedException();
    }

    // zwraca wszystkie książki jako listę
    public List<Book> GetBooks()
    {
        return _books.Values.ToList();
    }

    // dodaje nową książkę do słownika
    public void AddBook(Book book)
    {
         _books.Add(++i, book);
    }

    // aktualizuje istniejącą książkę – jeśli nie ma, zwraca false
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

    // usuwa książkę po Id – jeśli nie ma, zwraca false
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

    // pobiera książkę po Id – jeśli brak, zwraca null
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