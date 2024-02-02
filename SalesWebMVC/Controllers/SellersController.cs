using Microsoft.AspNetCore.Mvc;

namespace SalesWebMVC.Controllers;

public class SellersController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}