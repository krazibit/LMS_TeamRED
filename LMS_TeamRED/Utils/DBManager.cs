using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using LibraryManagementSystemDAL;
using LibraryManagementSystemDAL.Data;

namespace LibraryManagementSystem.Utils
{
    public class DBManager
    {
        private static DBManager _instance = null;

        private Dictionary<book, int> AggregateBooksByIsbnAvailability(List<book> bookList )
        {
            var aggregatedBookMap = new Dictionary<book, int>();
            
            var booksGroupByIsbn = bookList.GroupBy(b => new { b.Isbn });
            var isbnAvailability = new Dictionary<string, int>();
            foreach (var grp in booksGroupByIsbn)
            {
                isbnAvailability[grp.Key.Isbn] = grp.Count(g => g.Available);
            }

            foreach (var key in isbnAvailability)
            {
                book book = bookList.FirstOrDefault(b => b.Isbn == key.Key);
                if (book != null)
                    aggregatedBookMap[book] = key.Value;
            }

            return aggregatedBookMap;
        }

        public static DBManager Instance
        {
            get { return _instance ?? (_instance = new DBManager()); }
        }

        public void AddBook(book newBook)
        {
            using (var dbModel = new LMSDataModelEntities())
            {
                dbModel.books.AddObject(newBook);
                dbModel.ObjectStateManager.ChangeObjectState(newBook, System.Data.EntityState.Added);
                var saveCount = dbModel.SaveChanges();
                if (saveCount < 1)
                {
                    throw new Exception("Fail to insert Book " + newBook.Title + " in to DB");
                }
            }
        }

        public Dictionary<book, int> GetBooksByISBN(string isbn)
        {
 
          
            using (var dbModel = new LMSDataModelEntities())
            {
                try
                {
                    var books = dbModel.books.
                        Include(typeof(department).Name).
                        Include(typeof(publisher).Name).
                        Include(typeof(systemuser).Name).
                        Include(dbModel.bookauthors.EntitySet.Name).
                        Where(b => b.Isbn.Contains(isbn)).ToList();
                    return AggregateBooksByIsbnAvailability(books);
                }
                catch(Exception)
                {
                    return null;
                }
               
            }
            
           
        }


        public Dictionary<book, int> GetBooksByTitle(string title)
        {   
            using (var dbModel = new LMSDataModelEntities())
            {
                try
                {
                    var books = dbModel.books.
                        Include(typeof(department).Name).
                        Include(typeof(publisher).Name).
                        Include(typeof(systemuser).Name).
                        Include(dbModel.bookauthors.EntitySet.Name).
                        Where(b => b.Title.Contains(title)).ToList();
                    return AggregateBooksByIsbnAvailability(books);
                }
                catch (Exception ex)
                {
                    
                    return null;
                }
            }

            
           
        }

        public Dictionary<book, int> GetBooksByPublisher(string publisher)
        {
            using (var dbModel = new LMSDataModelEntities())
            {
                try
                {
                    var books = dbModel.books.
                        Include(typeof(department).Name).
                        Include(typeof(publisher).Name).
                        Include(typeof(systemuser).Name).
                        Include(dbModel.bookauthors.EntitySet.Name).
                        Where(b => b.publisher.Name.Contains(publisher)).ToList();
                    return AggregateBooksByIsbnAvailability(books);
                }
                catch (Exception)
                {
                    return null;
                }
            }
           
        }

        public Dictionary<book, int> GetBooksByAuthor(string authorName)
        {
         
            using (var dbModel = new LMSDataModelEntities())
            {
                try
                {
                    var authors =
                        dbModel.authors.Where(a => a.LastName.Contains(authorName) || a.FirstName.Contains(authorName)).
                            ToList();

                    var books = new List<book>();
                    foreach (var author in authors)
                    {
                        foreach (var bookAuthor in dbModel.bookauthors.Where(ba => (ba.AuthorId == author.Id)).ToList())
                            books.AddRange(dbModel.books.
                                                        Include(typeof(department).Name).
                                                        Include(typeof(publisher).Name).
                                                        Include(typeof(systemuser).Name).
                                                        Include(dbModel.bookauthors.EntitySet.Name).
                                                        Where(b => b.Id ==
                                                                                            bookAuthor.BookId).ToList());
                    }
                    return AggregateBooksByIsbnAvailability(books);
                }
                catch (Exception)
                {
                    return null;
                }
            }
           
        }


        public book GetAvailableBookByISBN(string isbn)
        {
           
            using (var dbModel = new LMSDataModelEntities())
            {
               return dbModel.books.
                                    Include(typeof(department).Name).
                                    Include(typeof(publisher).Name).
                                    Include(typeof(systemuser).Name).
                                    Include(dbModel.bookauthors.EntitySet.Name).
                                    FirstOrDefault(b => b.Isbn.Equals(isbn, StringComparison.OrdinalIgnoreCase) && b.Available);
            }
          
        }

        public book GetAvailableBookByID(int id)
        {
           
            using (var dbModel = new LMSDataModelEntities())
            {
               return dbModel.books.
                                    Include(typeof(department).Name).
                                    Include(typeof(publisher).Name).
                                    Include(typeof(systemuser).Name).
                                    FirstOrDefault(b => b.Id == id && b.Available);
            }
           
        }
        
        public void UpdateBook(book bookToUpdate)
        {
            using (var dbModel = new LMSDataModelEntities())
            {
                bookToUpdate.bookauthors = null;
                bookToUpdate.department = null;
                bookToUpdate.publisher = null;
                bookToUpdate.studentbookloans = null;
                bookToUpdate.systemuser = null;
                dbModel.books.AddObject(bookToUpdate);
                dbModel.ObjectStateManager.ChangeObjectState(bookToUpdate, System.Data.EntityState.Modified);
                var saveCount = dbModel.SaveChanges();
                if (saveCount < 1)
                {
                    throw new Exception("Fail to Update Book " + bookToUpdate.Title + "in DB");
                }
            }

        }

        
        public void AddStudentBookLoan( studentbookloan newLoan)
        {
            using (var dbModel = new LMSDataModelEntities())
            {
                dbModel.studentbookloans.AddObject(newLoan);
                dbModel.ObjectStateManager.ChangeObjectState(newLoan, System.Data.EntityState.Added);
                var saveCount = dbModel.SaveChanges();
                if (saveCount < 1)
                {
                    throw new Exception("Fail to insert Book loan " + newLoan.book.Title + " in to DB");
                }
            }
        }
        
        public List<studentbookloan> GetDueStudentBookLoans()
        {
            
            using(var dbModel = new LMSDataModelEntities())
            {
                try
                {
                    List<studentbookloan> dueStudentBookLoans = null;
                    dueStudentBookLoans = dbModel.studentbookloans.
                                                                    Include(typeof(book).Name).
                                                                    Include(typeof(student).Name).
                                                                    Include(typeof(systemuser).Name).
                                                                    Where(loan => loan.DueDate <= DateTime.Now).ToList();
                    return dueStudentBookLoans;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public List<studentbookloan> GetCurrentStudentBookLoans()
        {
           
            using (var dbModel = new LMSDataModelEntities())
            {
                try
                {
                    List<studentbookloan> studentBookLoans = null;
                    studentBookLoans =
                        dbModel.studentbookloans.
                                                    Include(typeof(book).Name).
                                                    Include(typeof(student).Name).
                                                    Include(typeof(systemuser).Name).   
                                                    Where(loan => loan.ReturnDate < DateTime.Now.AddYears(-10) || loan.DueDate <= DateTime.Now).ToList();
                    return studentBookLoans;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public List<studentbookloan> GetCurrentStudentBookLoansByISBN(string isbn)
        {
           
            using (var dbModel = new LMSDataModelEntities())
            {
                try
                {
                    List<studentbookloan> studentBookLoans = null;
                    studentBookLoans =
                        dbModel.studentbookloans.
                                                   Include(typeof(book).Name).
                                                   Include(typeof(student).Name).
                                                   Include(typeof(systemuser).Name).
                                                   Where(
                                                        loan =>
                                                        loan.book.Isbn.Equals(isbn, StringComparison.OrdinalIgnoreCase) && loan.ReturnDate == null).
                                                        ToList();
                    return studentBookLoans;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public List<studentbookloan> GetCurrentStudentBookLoansByStudentReg(string studentRegId)
        {
           
            using (var dbModel = new LMSDataModelEntities())
            {
                try
                {
                    List<studentbookloan> studentBookLoans = null;
                    studentBookLoans =
                        dbModel.studentbookloans.
                                                Include(typeof(book).Name).
                                                Include(typeof(student).Name).
                                                Include(typeof(systemuser).Name).
                                                Where(
                                                    loan =>
                                                    loan.student.RegistrationID.Equals(studentRegId, StringComparison.OrdinalIgnoreCase) &&
                                                    (loan.ReturnDate == DateTime.MinValue || loan.DueDate < DateTime.Now)).ToList();
                    foreach (var studentBookLoan in studentBookLoans)
                    {
                        studentBookLoan.book.publisher =
                            dbModel.publishers.FirstOrDefault(p => p.Id == studentBookLoan.book.PublisherID);
                    }
                    
                    return studentBookLoans;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }


        public studentbookloan GetStudentBookLoanByID(int loanID)
        {
           
                using (var dbModel = new LMSDataModelEntities())
                {

                  return dbModel.studentbookloans.
                            Include(typeof (book).Name).
                            Include(typeof (student).Name).
                            Include(typeof (systemuser).Name).
                            FirstOrDefault(
                                loan =>
                                loan.id==loanID);
                }
            }
        public void AddPublisher(publisher newPublisher)
        {
            using (var dbModel = new LMSDataModelEntities())
            {
                dbModel.publishers.AddObject(newPublisher);
                dbModel.ObjectStateManager.ChangeObjectState(newPublisher, System.Data.EntityState.Added);
                var savecount = dbModel.SaveChanges();
                if (savecount < 1)
                {
                    throw new Exception("Fail to insert Publisher " + newPublisher.Name + " into DB");
                }
            }
        }



        public void AddAuthor(author newAuthor)
        {
            using (var dbModel = new LMSDataModelEntities())
            {
                dbModel.authors.AddObject(newAuthor);
                dbModel.ObjectStateManager.ChangeObjectState(newAuthor, System.Data.EntityState.Added);
                var saveCount = dbModel.SaveChanges();
                if (saveCount < 1)
                {
                    throw new Exception("Fail to insert Author " + newAuthor.FirstName +" " +newAuthor.LastName + " into DB");
                }
            }
        }


        public void AddSystemUser(systemuser newUser)
        {
            using (var dbModel = new LMSDataModelEntities())
            {
                dbModel.systemusers.AddObject(newUser);
                dbModel.ObjectStateManager.ChangeObjectState(newUser, System.Data.EntityState.Added);
                var saveCount = dbModel.SaveChanges();
                if (saveCount < 1)
                {
                    throw new Exception("Fail to insert system user " + newUser.FirstName + " " + newUser.LastName + " into DB");
                }
            }
        }


        public void AddBookLoan(studentbookloan newBookLoan)
        {
            using (var dbModel = new LMSDataModelEntities())
            {
                dbModel.studentbookloans.AddObject(newBookLoan);
                dbModel.ObjectStateManager.ChangeObjectState(newBookLoan, System.Data.EntityState.Added);
                var saveCount = dbModel.SaveChanges();
                if (saveCount < 1)
                {
                    throw new Exception("Fail to insert new book loan " + newBookLoan.book.Title + " into DB");
                }
            }
        }


        public void AddDepartment(department newDepartment)
        {
            using (var dbModel = new LMSDataModelEntities())
            {
                dbModel.departments.AddObject(newDepartment);
                dbModel.ObjectStateManager.ChangeObjectState(newDepartment, System.Data.EntityState.Added);
                var saveCount = dbModel.SaveChanges();
                if (saveCount < 1)
                {
                    throw new Exception("Fail to insert new department " + newDepartment.Name + " into DB");
                }
            }
        }

        public void AddBookAuthor(bookauthor newBookAuthor)
        {
            using (var dbModel = new LMSDataModelEntities())
            {
                dbModel.bookauthors.AddObject(newBookAuthor);
                dbModel.ObjectStateManager.ChangeObjectState(newBookAuthor, System.Data.EntityState.Added);
                var saveCount = dbModel.SaveChanges();
                if (saveCount < 1)
                {
                    throw new Exception("Fail to insert new book author " + newBookAuthor.author.FirstName + " " +newBookAuthor.book.Title + " into DB");
                }
            }
        }



        public student GetStudentByRegNo(string regID)
        {
            using (var dbModel = new LMSDataModelEntities())
            {
                return dbModel.students.
                                   Include(typeof(department).Name).
                                   Include(typeof(sex).Name).
                                   Include(dbModel.studentbookloans.EntitySet.Name).
                                   FirstOrDefault(s => s.RegistrationID == regID);
            }
        }

        public void UpdateStudentBookLoan(studentbookloan loanToUpdate)
        {
            using (var dbModel = new LMSDataModelEntities())
            {
                //dbModel.Refresh(RefreshMode.ClientWins, dbModel.studentbookloans);
                loanToUpdate.book = null;
                loanToUpdate.student = null;
                loanToUpdate.systemuser = null;
                dbModel.studentbookloans.AddObject(loanToUpdate);
                dbModel.ObjectStateManager.ChangeObjectState(loanToUpdate, System.Data.EntityState.Modified);
                var saveCount = dbModel.SaveChanges();
                if (saveCount < 1)
                {
                    throw new Exception("Fail to Update Loan in DB");
                }
            }
        }
    }
}