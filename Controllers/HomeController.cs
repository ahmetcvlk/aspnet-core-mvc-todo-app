using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var model = _context.TodoItems.ToList();
        return View(model);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(TodoItem model)
    {
        if (ModelState.IsValid)
        {
            _context.TodoItems.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
