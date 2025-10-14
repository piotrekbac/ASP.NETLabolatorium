using Lab0.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab0.Controllers;

public class CalculatorController : Controller
{
    // GET
    public IActionResult Form()
    {
        return View();
    }
    
    public IActionResult Result(CalculatorModel model)
    {
        
        // Sprawdzamy czy a oraz b sa null
        if (!model.IsValid())
        {
            return View("Error", "Nie można obliczyć");
        }
        
        // w op może wystapić: add, sub, mul, div
        
        ViewBag.Result = model.Result();
        return View();
    }
}