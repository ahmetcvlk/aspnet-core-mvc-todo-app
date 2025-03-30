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

    public IActionResult Index(string filter = "all")
    {
        ViewBag.CurrentFilter = filter;
        var query = _context.TodoItems.AsQueryable();
        query = filter switch{
            "completed" => query.Where(x => x.IsDone),
            "active" => query.Where(x => !x.IsDone),
            _ => query.OrderBy(x => x.IsDone).ThenByDescending(x => x.CreatedAt)
        };

        var model = query.ToList();
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

    public IActionResult Delete(int id)
    {
        var item = _context.TodoItems.Find(id);
        if (item != null)
        {
            _context.TodoItems.Remove(item);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        var item = _context.TodoItems.Find(id);
        if (item == null)
        {
            return NotFound();
        }
        return View(item);
    }

    [HttpPost]
    public IActionResult Edit(TodoItem model)
    {
        if (ModelState.IsValid)
        {
            _context.TodoItems.Update(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(model);
    }

    public IActionResult Complete(int id)
    {
        var item = _context.TodoItems.Find(id);
        if (item != null)
        {
            item.IsDone = true;
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
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
