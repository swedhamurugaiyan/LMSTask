using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Controllers;

public class CategoryController : Controller
{

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult MainIndex()
    {
        return View();
    }
}
