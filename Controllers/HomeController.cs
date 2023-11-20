using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChefsNDishes.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefsNDishes.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    //* CHEF ROUTES
    //! Index -> View All Chefs
    [HttpGet("")]
    public IActionResult Index()
    {
        List<Chef> AllChefs = _context.Chefs.Include(c => c.AllDishes).ToList();
        return View(AllChefs);
    }

    //! Add Chef View - FORM
    [HttpGet("chefs/new")]
    public ViewResult NewChef()
    {
        return View();
    }

    //! Create New Chef
    [HttpPost("/chefs/create")]
    public IActionResult CreateChef(Chef newChef)
    {
        if (!ModelState.IsValid)
        {
            return View("NewChef");
        }

        _context.Add(newChef);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }


    //* DISH ROUTES
    //! View All Dishes
    [HttpGet("dishes")]
    public ViewResult Dishes()
    {
        List<Dish> AllDishes = _context.Dishes.Include(d => d.Creator).ToList();
        return View(AllDishes);
    }

    //! Add Dish View - FORM
    [HttpGet("dishes/new")]
    public ViewResult NewDish()
    {
        ViewBag.AllChefs = _context.Chefs.ToList();
        return View("NewDish");
    }

    //! Create New Dish
    [HttpPost("/dishes/create")]
    public IActionResult CreateDish(Dish newDish)
    {
        if (!ModelState.IsValid)
        {
            return NewDish();
        }

        _context.Add(newDish);
        _context.SaveChanges();

        return RedirectToAction("Dishes");
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}