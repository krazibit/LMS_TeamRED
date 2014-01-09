using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models.BookModels;

namespace LibraryManagementSystem.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/
        [HttpGet]
        public ActionResult Index()
        {
            //ViewData["IsEmpty"] = "1"
            return View();
        }

        [HttpPost]
        public ActionResult Index(SearchBookModel model)
        {
            var books = new List<BookModel> {new BookModel(), new BookModel(), new BookModel()};
            return View(books);
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
