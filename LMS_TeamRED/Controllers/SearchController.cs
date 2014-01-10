using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LibraryManagementSystem.Models.BookModels;
using LibraryManagementSystem.Utils;
using LibraryManagementSystemDAL.Data;

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
            ViewData["Queried"] = false;
            return View(new SearchBookModel());
        }

        [HttpPost]
        public ActionResult Index(SearchBookModel model)
        {
            var books = new Dictionary<Book, int>();
            int searchType = (int) model.SearchType;
            switch (searchType)
            {
                case (int) BookSearchType.Title:
                    {
                      books = DBManager.Instance.GetBooksByTitle(model.SearchString);
                      break;  
                    }
                case (int) BookSearchType.Isbn:
                    {
                        books = DBManager.Instance.GetBooksByISBN(model.SearchString);
                        break;
                    }
                case (int) BookSearchType.Publisher:
                    {
                        books = DBManager.Instance.GetBooksByPublisher(model.SearchString);
                        break;
                    }
                    case (int) BookSearchType.Author:
                    {
                        books = DBManager.Instance.GetBooksByAuthor(model.SearchString);
                        break;
                    }

                default:
                    {
                        break;
                    }
            }
            ViewData["BookMap"] = books;
            ViewData["Queried"] = true;
            return View(model);
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
