using Lab0.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab0.Controllers;

[ApiController]
[Route("api/publishers")] // URL: /api/publishers
// w późniejszym etapie testowałem API po tym URL: http://localhost:5123/api/publishers?filter=Ekstra
public class ApiPublishersController(AddDbContext context) : ControllerBase
{
    [HttpGet]
    public IActionResult GetPublishers(string? filter)
    {
        var query = context.Publishers.AsQueryable();

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