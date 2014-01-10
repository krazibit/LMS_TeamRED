using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryManagementSystemDAL;
using LibraryManagementSystemDAL.Data;

namespace LibraryManagementSystem.Utils
{
    public class DBManager
    {
        private static DBManager _instance = null;

        private Dictionary<Book, int> AggregateBooksByIsbnAvailability(List<Book> bookList )
        {
            var aggregatedBookMap = new Dictionary<Book, int>();
            
            var booksGroupByAvailability = bookList.GroupBy(b => new { b.Isbn });
            var isbnAvailability = new Dictionary<string, int>();
            foreach (var grp in booksGroupByAvailability)
            {
                isbnAvailability[grp.Key.Isbn] = grp.Select(g => g.Available == true).Count();
            }

            foreach (var key in isbnAvailability)
            {
                Book book = bookList.FirstOrDefault(b => b.Isbn == key.Key);
                if (book != null)
                    aggregatedBookMap[book] = key.Value;
            }

            return aggregatedBookMap;
        }



        public static DBManager Instance
        {
            get { return _instance ?? (_instance = new DBManager()); }
        }

        public void AddBook(Book newBook)
        {
            using (var dbModel = new LMSDataModelEntities())
            {
                dbModel.Books.AddObject(newBook);
                dbModel.ObjectStateManager.ChangeObjectState(newBook, System.Data.EntityState.Added);
                var saveCount = dbModel.SaveChanges();
                if (saveCount < 1)
                {
                    throw new Exception("Fail to insert Book " + newBook.Title + " in to DB");
                }
            }
        }

        public Dictionary<Book, int> GetBooksByISBN(string isbn)
        {
 
          
            using (var dbModel = new LMSDataModelEntities())
            {
                try
                {
                    var books = dbModel.Books.Where(b => b.Isbn.Equals(isbn, StringComparison.OrdinalIgnoreCase)).ToList();
                    return AggregateBooksByIsbnAvailability(books);
                }
                catch(Exception)
                {
                    return null;
                }
               
            }
            
           
        }


        public Dictionary<Book, int> GetBooksByTitle(string title)
        {   
            using (var dbModel = new LMSDataModelEntities())
            {
                try
                {
                    var books = dbModel.Books.Where(b => b.Title.Contains(title)).ToList();
                    return AggregateBooksByIsbnAvailability(books);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            
           
        }

        public Dictionary<Book, int> GetBooksByPublisher(string publisher)
        {
            using (var dbModel = new LMSDataModelEntities())
            {
                try
                {
                    var books = dbModel.Books.Where(b => b.publisher.Name.Contains(publisher)).ToList();
                    return AggregateBooksByIsbnAvailability(books);
                }
                catch (Exception)
                {
                    return null;
                }
            }
           
        }

        public Dictionary<Book, int> GetBooksByAuthor(string authorName)
        {
         
            using (var dbModel = new LMSDataModelEntities())
            {
                try
                {
                    var authors =
                        dbModel.Authors.Where(a => a.LastName.Contains(authorName) || a.FirstName.Contains(authorName)).
                            ToList();

                    var books = new List<Book>();
                    foreach (var author in authors)
                    {
                        foreach (var bookAuthor in dbModel.BookAuthors.Where(ba => (ba.AuthorId == author.Id)).ToList())
                            books.AddRange(dbModel.Books.Where(b => b.Id ==
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


        public Book GetAvailableBookByISBN(string isbn)
        {
            Book book = null;
            using (var dbModel = new LMSDataModelEntities())
            {
                book = dbModel.Books.FirstOrDefault(b => b.Isbn.Equals(isbn, StringComparison.OrdinalIgnoreCase) && b.Available == true);
            }
            return book;
        }

        public void UpdateBook(Book bookToUpdate)
        {
            using (var dbModel = new LMSDataModelEntities())
            {
                dbModel.Books.AddObject(bookToUpdate);
                dbModel.ObjectStateManager.ChangeObjectState(bookToUpdate, System.Data.EntityState.Modified);
                var saveCount = dbModel.SaveChanges();
                if (saveCount < 1)
                {
                    throw new Exception("Fail to Update Book " + bookToUpdate.Title + "in DB");
                }
            }

        }

        
        public void AddStudentBookLoan( StudentBookLoan newLoan)
        {
            using (var dbModel = new LMSDataModelEntities())
            {
                dbModel.StudentBookLoans.AddObject(newLoan);
                dbModel.ObjectStateManager.ChangeObjectState(newLoan, System.Data.EntityState.Added);
                var saveCount = dbModel.SaveChanges();
                if (saveCount < 1)
                {
                    throw new Exception("Fail to insert Book loan " + newLoan.book.Title + " in to DB");
                }
            }
        }
        
        public List<StudentBookLoan> GetDueStudentBookLoans()
        {
            
            using(var dbModel = new LMSDataModelEntities())
            {
                try
                {
                    List<StudentBookLoan> dueStudentBookLoans = null;
                    dueStudentBookLoans = dbModel.StudentBookLoans.Where(loan => loan.DueDate >= DateTime.Now).ToList();
                    return dueStudentBookLoans;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public List<StudentBookLoan> GetCurrentStudentBookLoans()
        {
           
            using (var dbModel = new LMSDataModelEntities())
            {
                try
                {
                    List<StudentBookLoan> studentBookLoans = null;
                    studentBookLoans =
                        dbModel.StudentBookLoans.Where(loan => loan.ReturnDate == null || loan.DueDate >= DateTime.Now).
                            ToList();
                    return studentBookLoans;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public List<StudentBookLoan> GetCurrentStudentBookLoansByISBN(string isbn)
        {
           
            using (var dbModel = new LMSDataModelEntities())
            {
                try
                {
                    List<StudentBookLoan> studentBookLoans = null;
                    studentBookLoans =
                        dbModel.StudentBookLoans.Where(
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

        public List<StudentBookLoan> GetCurrentStudentBookLoansByStudentReg(string studentRegId)
        {
           
            using (var dbModel = new LMSDataModelEntities())
            {
                try
                {
                    List<StudentBookLoan> studentBookLoans = null;
                    studentBookLoans =
                        dbModel.StudentBookLoans.Where(
                            loan =>
                            loan.student.RegistrationID.Equals(studentRegId, StringComparison.OrdinalIgnoreCase) &&
                            loan.ReturnDate == null).ToList();
                    return studentBookLoans;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }


        public void AddPublisher(Publisher newPublisher)
        {
            using (var dbModel = new LMSDataModelEntities())
            {
                dbModel.Publishers.AddObject(newPublisher);
                dbModel.ObjectStateManager.ChangeObjectState(newPublisher, System.Data.EntityState.Added);
                var savecount = dbModel.SaveChanges();
                if (savecount < 1)
                {
                    throw new Exception("Fail to insert Publisher " + newPublisher.Name + " into DB");
                }
            }
        }



        public void AddAuthor(Author newAuthor)
        {
            using (var dbModel = new LMSDataModelEntities())
            {
                dbModel.Authors.AddObject(newAuthor);
                dbModel.ObjectStateManager.ChangeObjectState(newAuthor, System.Data.EntityState.Added);
                var saveCount = dbModel.SaveChanges();
                if (saveCount < 1)
                {
                    throw new Exception("Fail to insert Author " + newAuthor.FirstName +" " +newAuthor.LastName + " into DB");
                }
            }
        }


        public void AddSystemUser(SystemUser newUser)
        {
            using (var dbModel = new LMSDataModelEntities())
            {
                dbModel.SystemUsers.AddObject(newUser);
                dbModel.ObjectStateManager.ChangeObjectState(newUser, System.Data.EntityState.Added);
                var saveCount = dbModel.SaveChanges();
                if (saveCount < 1)
                {
                    throw new Exception("Fail to insert system user " + newUser.FirstName + " " + newUser.LastName + " into DB");
                }
            }
        }


        public void AddBookLoan(StudentBookLoan newBookLoan)
        {
            using (var dbModel = new LMSDataModelEntities())
            {
                dbModel.StudentBookLoans.AddObject(newBookLoan);
                dbModel.ObjectStateManager.ChangeObjectState(newBookLoan, System.Data.EntityState.Added);
                var saveCount = dbModel.SaveChanges();
                if (saveCount < 1)
                {
                    throw new Exception("Fail to insert new book loan " + newBookLoan.book.Title + " into DB");
                }
            }
        }


        public void AddDepartment(Department newDepartment)
        {
            using (var dbModel = new LMSDataModelEntities())
            {
                dbModel.Departments.AddObject(newDepartment);
                dbModel.ObjectStateManager.ChangeObjectState(newDepartment, System.Data.EntityState.Added);
                var saveCount = dbModel.SaveChanges();
                if (saveCount < 1)
                {
                    throw new Exception("Fail to insert new department " + newDepartment.Name + " into DB");
                }
            }
        }

        public void AddBookAuthor(BookAuthor newBookAuthor)
        {
            using (var dbModel = new LMSDataModelEntities())
            {
                dbModel.BookAuthors.AddObject(newBookAuthor);
                dbModel.ObjectStateManager.ChangeObjectState(newBookAuthor, System.Data.EntityState.Added);
                var saveCount = dbModel.SaveChanges();
                if (saveCount < 1)
                {
                    throw new Exception("Fail to insert new book author " + newBookAuthor.author.FirstName + " " +newBookAuthor.book.Title + " into DB");
                }
            }
        }


    }
}