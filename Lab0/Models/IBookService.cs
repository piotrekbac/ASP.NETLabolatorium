namespace Lab0.Models;

public interface IBookService
{
    // zwracamy liste obiektów
    List<Book> GetBooks();
    
    // tworzenie nowej ksiązki (argumentem jest klasa naszego modelu)
    void AddBook(Book book);
    
    // zwracamy infomrację czy update się powiódł 
    bool UpdateBook(Book book);
    
    // tak samo zwracamy informację czy usunięcie obiektu się powiodło (robimy to po id naszego obiektu0
    bool DeleteBook(int Id);
    
    // szukamy obiektu po Id (dajemy typ nullable za pomocą -> ?)
    Book? GetBookById(int Id);
}