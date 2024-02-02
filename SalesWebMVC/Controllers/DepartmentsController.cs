using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;

namespace SalesWebMVC.Controllers;

public class DepartmentsController : Controller
{
    // GET
    public IActionResult Index()
    {
        List<Department> departments =
        [
            new Department() { Id = 1, Name = "Electronics" },
            new Department() { Id = 2, Name = "Fashion" }
        ];
        
        return View(departments);
    }
}