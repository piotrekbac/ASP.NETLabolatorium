using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab0.Models;

namespace Lab0.Controllers;

[Authorize]
public class PublisherController(AddDbContext context) : Controller
{
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        return View(await context.Publishers.ToListAsync());
    }

    [AllowAnonymous]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var publisher = await context.Publishers
            .FirstOrDefaultAsync(m => m.PublisherId == id);
            
        if (publisher == null) return NotFound();

        return View(publisher);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Publisher publisher)
    {
        if (ModelState.IsValid)
        {
            context.Add(publisher);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(publisher);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var publisher = await context.Publishers.FindAsync(id);
        if (publisher == null) return NotFound();
        
        return View(publisher);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Publisher publisher)
    {
        if (id != publisher.PublisherId) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(publisher);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.Publishers.Any(e => e.PublisherId == id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(publisher);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var publisher = await context.Publishers
            .FirstOrDefaultAsync(m => m.PublisherId == id);
            
        if (publisher == null) return NotFound();

        return View(publisher);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var publisher = await context.Publishers.FindAsync(id);
        if (publisher != null)
        {
            context.Publishers.Remove(publisher);
            await context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}