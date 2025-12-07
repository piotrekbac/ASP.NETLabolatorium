using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab0.Models;

namespace Lab0.Controllers;

// [Authorize] – cały kontroler wymaga zalogowania, inaczej użytkownik nie ma dostępu
[Authorize]
public class PublisherController(AddDbContext context) : Controller
{
    // [AllowAnonymous] – lista wydawców dostępna publicznie, bez logowania
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        // pobieramy wszystkich wydawców z bazy i przekazujemy do widoku
        return View(await context.Publishers.ToListAsync());
    }

    // szczegóły wydawcy też są dostępne dla każdego
    [AllowAnonymous]
    public async Task<IActionResult> Details(int? id)
    {
        // jeśli nie podano id, zwracamy błąd 404
        if (id == null) return NotFound();

        // szukamy wydawcy po kluczu głównym
        var publisher = await context.Publishers
            .FirstOrDefaultAsync(m => m.PublisherId == id);
            
        // jeśli brak w bazie – błąd 404
        if (publisher == null) return NotFound();

        // jeśli znaleziono – pokazujemy szczegóły
        return View(publisher);
    }

    // GET – wyświetlamy pusty formularz dodawania wydawcy
    public IActionResult Create()
    {
        return View();
    }

    // POST – zapisujemy nowego wydawcę
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Publisher publisher)
    {
        // walidacja modelu – jeśli poprawny, zapisujemy
        if (ModelState.IsValid)
        {
            context.Add(publisher);
            await context.SaveChangesAsync();
            
            // po dodaniu wracamy na listę
            return RedirectToAction(nameof(Index));
        }
        
        // jeśli walidacja nie przeszła – wracamy do formularza
        return View(publisher);
    }

    // GET – formularz edycji istniejącego wydawcy
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var publisher = await context.Publishers.FindAsync(id);
        if (publisher == null) return NotFound();
        
        return View(publisher);
    }

    // POST – zapisujemy zmiany w wydawcy
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Publisher publisher)
    {
        // sprawdzamy czy id z adresu zgadza się z id w modelu
        if (id != publisher.PublisherId) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                // aktualizacja danych w bazie
                context.Update(publisher);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // jeśli ktoś usunął wydawcę w międzyczasie – błąd 404
                if (!context.Publishers.Any(e => e.PublisherId == id)) return NotFound();
                
                // w innym przypadku rzucamy wyjątek dalej
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        
        // jeśli walidacja nie przeszła – wracamy do formularza
        return View(publisher);
    }

    // GET – strona z potwierdzeniem usunięcia
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var publisher = await context.Publishers
            .FirstOrDefaultAsync(m => m.PublisherId == id);
            
        if (publisher == null) return NotFound();

        return View(publisher);
    }

    // POST – faktyczne usunięcie wydawcy po potwierdzeniu
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