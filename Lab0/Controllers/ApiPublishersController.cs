using Lab0.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab0.Controllers;

// Kontroler API służący do obsługi zapytań asynchronicznych z JavaScript (Fetch API) - zwraca dane w formacie JSON
[ApiController]
[Route("api/publishers")] // URL: /api/publishers
public class ApiPublishersController(AddDbContext context) : ControllerBase
{
    [HttpGet]
    public IActionResult GetPublishers(string? filter)
    {
        var query = context.Publishers.AsQueryable();
        
        // Prosta logika filtrowania po nazwie dla dynamicznej wyszukiwarki
        if (!string.IsNullOrEmpty(filter))
        {
            query = query.Where(p => p.Name.ToLower().Contains(filter.ToLower()));
        }

        // Zwracamy tylko ID i Nazwę
        var result = query
            .Select(p => new { id = p.PublisherId, name = p.Name })
            .Take(20)
            .ToList();

        return Ok(result);
    }
}