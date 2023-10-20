using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NOGAsteWebApp.Models;

namespace NOGAsteWebApp.Controllers
{

    //      you-are-here
    //xxx> HomeController>  xxx

    //HomeController inherits from MVC "Controller"
    //Only displays Index.cshtml and Privacy.cshtml

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //Action Methods view() built-in method from the Controller bass class
        //that rendes a view
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

  
    }//
}
