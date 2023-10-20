using Microsoft.AspNetCore.Mvc;
using NOGAsteWebApp.Models;
using System.Collections.Generic;

namespace NOGAsteWebApp.Controllers
{
    public class TextAreaConsoleController : Controller
    {
        public IActionResult TextAreaConsole()
        {
            var textAreaConsole = new TextAreaConsoleModel();
            return View(textAreaConsole);
        }

    }
}

