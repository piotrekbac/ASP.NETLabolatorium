using Lab0.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab0.Controllers;

public class BookController : Controller
{
    private static Dictionary<int, Book> _books = new()
    {
        {1, new Book() {Id = 1, Author = "Maacieeek", Title = "Zakochany kundel"}}
    };

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

    [HttpGet]
    public IActionResult Details(int id)
    {
        if (_books.ContainsKey(id))
        {
            return View(_books[id]);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        if (_books.ContainsKey(id))
        {
            return View(_books[id]);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost] //dodajemy tutaj HttpPost żeby zamiast formularza odbierać żądanie z odpwiednim czasownikiem. Model odpowiada opisowi w formularzu
    public IActionResult Edit(Book model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        // aktualizacja obiektu
        _books[model.Id] =  model;        
        return RedirectToAction("Index"); 
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        if (_books.ContainsKey(id))
        {
            return View(_books[id]);
        }
        else
        {
            return NotFound();  // w przypadku jakbyśmy chcieli usunąć coś czego nie ma 
        }
    }

    [HttpPost]
    public IActionResult DeleteConfirm(int id)
    {
        _books.Remove(id);
        return RedirectToAction("Index");   // po akcji wracamy do listy obiektów
    }
    
}