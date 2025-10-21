using Lab0.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab0.Controllers;

public class BookController : Controller
{
    private static Dictionary<int, Book> _books = new();

    private static int i = 0; 
    
    // GET
    public IActionResult Index()
    {
        return View(_books.Values.ToList());
    }

    
    [HttpGet]       // Wyświetlenie formularza dodania obiektu 
    public IActionResult Create()
    {
        return View();  
    }
    
    [HttpPost]       // Odbiór danych obiektu i zapisanie ich do bazy 
    public IActionResult Create(Book model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // zapisanie obiketu
        model.Id = ++i;                     // nadajemy modelowi odpowiednie id (zwiększając je o 1)
        _books.Add(model.Id, model);        // następnie odpowiednio dodajemy te modele i id. 
        return RedirectToAction("Index");   // przejdź do listy obiketów
    }
}