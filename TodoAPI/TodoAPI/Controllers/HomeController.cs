using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoAPI.Models;

namespace PersonOfTheYear.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        /*
        [HttpPost]
        public IActionResult Index(int id, int id)
        {
            return RedirectToAction("Results", new { startYear, endYear });
        }

        [HttpGet]
        public IActionResult Results(int startYear, int endYear)
        {
            List<Person> people = Person.GetPeopleFromCSV(startYear, endYear);
            return View(people);
        }*/
    }
}