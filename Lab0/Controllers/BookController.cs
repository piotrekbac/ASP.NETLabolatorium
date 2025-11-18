using Lab0.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab0.Controllers;

public class BookController(IBookService service) : Controller
{
    // GET
    public IActionResult Index()
    {
        return View(service.GetBooks());        // ważnym było tutaj dodać odpowiednią linijkę service.GetBooks początek LAB06
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

        service.AddBook(model);    
        return RedirectToAction("Index");   // przejdź do listy obiketów
    }


    public IActionResult Details(int id)
    {
        var book = service.GetBookById(id);
        {
            if (book is not null)
            {
                return View(book);
            }
            else
            {
                return NotFound();
            }
        }
    }
   

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var  book = service.GetBookById(id);
        if (book is not null)
        {
            return View(book);
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
        service.UpdateBook(model);     
        return RedirectToAction("Index"); 
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var book = service.GetBookById(id);
        if (book is not null)
        {
            return View(book);
        }
        else
        {
            return NotFound();  // w przypadku jakbyśmy chcieli usunąć coś czego nie ma 
        }
    }

    [HttpPost]
    public IActionResult DeleteConfirm(int id)
    {
        service.DeleteBook(id);
        return RedirectToAction("Index");   // po akcji wracamy do listy obiektów
    }
    
}