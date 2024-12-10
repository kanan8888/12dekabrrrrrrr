using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace Uniqlo.Controllers;

public class HomeController : Controller
{
    

    public IActionResult Index()
    {
        return View();
    }

  
}
