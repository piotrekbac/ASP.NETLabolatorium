using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Lab0.Models;

namespace Lab0.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    
    public IActionResult Calculator(double? a, double? b, [FromQuery(Name = "operator")]string op)
    {
        
        // Definiujemy działanie dodawnaia a oraz b
        string result = "";
        
        // Sprawdzamy czy a oraz b sa null
        if (a is null || b is null)
        {
            return View("Calculator", "Brak parametru a lub b");
        }
        
        // w op może wystapić: add, sub, mul, div
        switch (op)
        {
            case "add": result = $"{a} + {b} = {a + b}";
                break;
            case "sub": result = $"{a} - {b} = {a - b}";
                break;
            case "mul": result = $"{a} * {b} = {a * b}";
                break;
            case "div": result = $"{a} / {b} = {a / b}";
                break;
            default:
                result = "Nieznany operator!";
                break;
                
        }
        return View("Calculator",result);
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}