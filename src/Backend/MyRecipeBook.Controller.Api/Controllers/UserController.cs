using Microsoft.AspNetCore.Mvc;

namespace MyRecipeBook.Controller.Api.Controllers;

public class UserController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}