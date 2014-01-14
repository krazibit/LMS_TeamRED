using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models.BookModels;
using LibraryManagementSystem.Utils;
using LibraryManagementSystemDAL.Data;

namespace LibraryManagementSystem.Controllers
{
    public class ReturnBookController : Controller
    {
        //
        // GET: /ReturnBook/

        public ActionResult Index()
        {
            var model = new ReturnBookModel(); 
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(ReturnBookModel model)
        {
            var loanList = DBManager.Instance.GetCurrentStudentBookLoansByStudentReg(model.StudentReg);
            ViewData["LoanList"] = loanList;

            return View(model);
        }

        [HttpPost]
        public ActionResult ReturnProcessor(studentbookloan loanDetails)
        {
            loanDetails = DBManager.Instance.GetStudentBookLoanByID(loanDetails.id);
            var returnedBook = loanDetails.book;
            var regId = loanDetails.student.RegistrationID;
            returnedBook.Available = true;
            loanDetails.ReturnDate = DateTime.Now;
            try
            {
                DBManager.Instance.UpdateBook(returnedBook);
                DBManager.Instance.UpdateStudentBookLoan(loanDetails);
            }
            catch(Exception ex)
            {
                return View(new ReturnBookModel { ReturnSuccess = false, StudentReg = regId });
            }
            return View(new ReturnBookModel { ReturnSuccess = true, StudentReg = regId });
        }

    }
}
