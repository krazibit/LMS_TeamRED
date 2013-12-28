using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/
        [HttpGet]
        public ActionResult Index()
        {
            ViewData["IsEmpty"] = "1";
            return View();
        }

        [HttpPost]
        public ActionResult Index(String searchTerm, String searchType)
        {
            ViewData["IsEmpty"] = "1";
            return View();
        }


        [HttpGet]
        public ActionResult BorrowedBooks()
        {
            return View();
        }


        [HttpPost]
        public ActionResult BorrowedBooks(DateTime fromDate, DateTime toDate)
        {
            return View();
        }

        public ActionResult DueBooks()
        {
            return View();
        }
    }
}
