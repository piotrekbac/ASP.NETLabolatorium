using Lab0.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab0.Controllers;

// atrybut [Authorize] zabezpiecza cały kontroler - dostęp mają tylko zalogowani użytkownicy
[Authorize]
public class BookController(IBookService service, AddDbContext context) : Controller
{
    // [AllowAnnonymous] to wyjątek od reguły - lista książek jest publicznie dostepna
    [AllowAnonymous]
    public async Task<IActionResult> Index(int page = 1, int size = 10)
    {
        // pobieram dane przez warstwę serwisu, aby oddzielić logikę aplikacji od naszego kontrolera
        var pagedResult = await service.GetBooksPage(page, size);
        return View(pagedResult);
    }

    [HttpGet]
    public IActionResult Create()
    {
        // wyświetlamy pusty formularz dodawania książki
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(Book model)
    {
        // jeśli walidacja się nie powiodła, wracamy do formularza z błędami
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        // zapisujemy nową książkę przez serwis
        service.AddBook(model);    
        
        // po dodaniu wracamy na listę
        return RedirectToAction("Index");
    }
    
    // szczegóły książki są dostępne publicznie
    [AllowAnonymous]
    public IActionResult Details(int id)
    {
        var book = service.GetBookById(id);
        if (book is not null)
        {
            // jeśli książka istnieje, pokazujemy jej widok
            return View(book);
        }
        
        // w przeciwnym razie zwracamy bład 404
        return NotFound();
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var book = service.GetBookById(id);
        if (book is not null)
        {
            // formularz edycji wypełniony danymi książki
            return View(book);
        }
        return NotFound();
    }

    [HttpPost]
    public IActionResult Edit(Book model)
    {
        // walidacja – jeśli coś nie gra, wracamy do formularza
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        // aktualizacja danych książki
        service.UpdateBook(model);    
        
        // po edycji wracamy na listę
        return RedirectToAction("Index"); 
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var book = service.GetBookById(id);
        if (book is not null)
        {
            // pokazujemy stronę z potwierdzeniem usunięcia
            return View(book);
        }
        return NotFound();
    }

    [HttpPost]
    public IActionResult DeleteConfirm(int id)
    {
        // faktyczne usunięcie książki
        service.DeleteBook(id);
        
        // po usunięciu wracamy na listę
        return RedirectToAction("Index");
    }
}