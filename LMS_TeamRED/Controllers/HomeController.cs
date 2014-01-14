using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "Krazibit in the building!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult AddBook()
        {
            return View();
        }

        public ActionResult CheckStudentStatus()
        {
            return View();
        }
    }
}
