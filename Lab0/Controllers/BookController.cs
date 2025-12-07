using Lab0.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab0.Controllers;

[Authorize]
public class BookController(IBookService service, AddDbContext context) : Controller
{
    [AllowAnonymous]
    public async Task<IActionResult> Index(int page = 1, int size = 10)
    {
        var pagedResult = await service.GetBooksPage(page, size);
        return View(pagedResult);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(Book model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        service.AddBook(model);    
        return RedirectToAction("Index");
    }

    [AllowAnonymous]
    public IActionResult Details(int id)
    {
        var book = service.GetBookById(id);
        if (book is not null)
        {
            return View(book);
        }
        return NotFound();
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var book = service.GetBookById(id);
        if (book is not null)
        {
            return View(book);
        }
        return NotFound();
    }

    [HttpPost]
    public IActionResult Edit(Book model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
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
        return NotFound();
    }

    [HttpPost]
    public IActionResult DeleteConfirm(int id)
    {
        service.DeleteBook(id);
        return RedirectToAction("Index");
    }
}