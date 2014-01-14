using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystemDAL.Data;
using LibraryManagementSystem.Models.BookModels;
using LibraryManagementSystem.Utils;

namespace LibraryManagementSystem.Controllers
{
    public class LoanBookController : Controller
    {
        //
        // GET: /LoanBook/
        /*[HttpGet]
        public ActionResult Index(LoanBookModel model,bool nou =false)
        {
            / *if (loanSuccess != null)
            {
                ViewData["LoanSuccess"] = loanSuccess;
                ViewData["InitialCall"] = false;
            }
            else
                ViewData["InitialCall"] = true; * /
            return View(model);
        }*/


        [HttpPost]
        public ActionResult Index(LoanBookModel loanBookModel)
        {
            if (loanBookModel.IsInitial)
            {
               ModelState.Clear();
               ViewData["InitialCall"] = 1 ;
               return View(new LoanBookModel {BookId = loanBookModel.BookId, BookIsbn = loanBookModel.BookIsbn,BookTitle = loanBookModel.BookTitle, IsInitial = false});
            }
            ViewData["InitialCall"] = 0;
            var loanbook = DBManager.Instance.GetAvailableBookByID(loanBookModel.BookId);
            var loanee = DBManager.Instance.GetStudentByRegNo(loanBookModel.StudentReg);
            //TODO: Check Max Books Held;
            if (loanee == null)
            {
                ViewData["LoanSuccess"] = 0;
                return View(loanBookModel);
            }

            var bookLoanData = new studentbookloan
                                               {
                                                   BookId = loanBookModel.BookId,
                                                   DueDate = DateTime.Now.AddDays(7),
                                                   // Make Configurable parameter
                                                   IssuerID = 1,
                                                   StudentID = loanee.Id,
                                                   IssueDate = DateTime.Now
                                               };
            //Add Book Loan Data
            DBManager.Instance.AddStudentBookLoan(bookLoanData);

            loanbook.Available = false;
            
            //Update Book
            DBManager.Instance.UpdateBook(loanbook);

            ViewData["LoanSuccess"] = 1;    
           
            return View(loanBookModel);
        }
    }
}
