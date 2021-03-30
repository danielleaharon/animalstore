using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Animal_Store.Models;
using Microsoft.AspNetCore.Http;

namespace Animal_Store.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Type") == null || HttpContext.Session.GetString("Type") == "user")
            {
                HttpContext.Session.SetString("Type", "user");

                ViewBag.message = "Log in";

                //return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.message = null;
                ViewBag.message2 = "Hi " + HttpContext.Session.GetString("userName").ToString() + " Log out";

            }
            if (HttpContext.Session.GetString("Type") == "admin")
            {
                ViewBag.msg = "Customer";
            }
            else if (HttpContext.Session.GetString("Type") == "Owner")
            {

                ViewBag.MyPets = "My Pets";

            }
            else if (HttpContext.Session.GetString("Type") == "userRegistered")
            {

                ViewBag.wish = "wish list";

            }
            ViewBag.userId = HttpContext.Session.GetInt32("userID");

            return View();
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
        public IActionResult Contact()
        {
            ViewBag.userId = HttpContext.Session.GetInt32("userID");
            if (HttpContext.Session.GetString("Type") == null || HttpContext.Session.GetString("Type") == "user")
            {
                HttpContext.Session.SetString("Type", "user");

                ViewBag.message = "Log in";

                //return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.message = null;
                ViewBag.message2 = "Hi " + HttpContext.Session.GetString("userName").ToString() + " Log out";

            }
            if (HttpContext.Session.GetString("Type") == "admin")
            {
                ViewBag.msg = "Customer";
            }
            else if (HttpContext.Session.GetString("Type") == "Owner")
            {

                ViewBag.MyPets = "My Pets";

            }
            else if (HttpContext.Session.GetString("Type") == "userRegistered")
            {

                ViewBag.wish = "'wish list";

            }

            return View("Contact");
        }

    }
}
