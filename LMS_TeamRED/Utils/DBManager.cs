using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryManagementSystemDAL;

namespace LibraryManagementSystem.Utils
{
    public class DBManager
    {
        private static DBManager _instance = null;

        public static DBManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DBManager();
                }
                return _instance;
            }
        
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

        public List<Book> GetBooksByISBN(string isbn)
        {
            List<Book> books = null;
            using (var dbModel = new LMSDataModelEntities())
            {
               books = dbModel.Books.Where(b => b.Isbn.Equals(isbn, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return books;
        }
        
        public List<Book> GetBooksByTitle(string title)
        {
            List<Book> books = null;
            using (var dbModel = new LMSDataModelEntities())
            {
                books = dbModel.Books.Where(b => b.Title.Contains(title)).ToList();
            }
            return books;
        }
        
        public List<Book> GetBooksByPublisher(string publisher)
        {
            List<Book> books = null;
            using (var dbModel = new LMSDataModelEntities())
            {
                books = dbModel.Books.Where(b => b.publisher.Name.Contains(publisher)).ToList();
            }
            return books;
        }

        public List<Book> GetBooksByAuthor(string authorName)
        {
            List<Book> books = null;
            using (var dbModel = new LMSDataModelEntities())
            {
                List<Author> authors = dbModel.Authors.Where(a => a.LastName.Contains(authorName) || a.FirstName.Contains(authorName)).ToList();

                if (authors != null)
                {
                    books = new List<Book>();
                    foreach (var author in authors)
                    {
                        foreach(var bookAuthor in dbModel.BookAuthors.Where(ba => (ba.AuthorId == author.Id)).ToList())
                            books.AddRange(dbModel.Books.Where(b => b.Id ==
                                bookAuthor.BookId).ToList());
                    }
                }

            }
            return books;
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
            List<StudentBookLoan> dueStudentBookLoans = null;
            using(var dbModel = new LMSDataModelEntities())
            {
                dueStudentBookLoans = dbModel.StudentBookLoans.Where(loan => loan.DueDate >= DateTime.Now).ToList();
                return dueStudentBookLoans;
            }
        }

        public List<StudentBookLoan> GetCurrentStudentBookLoans()
        {
            List<StudentBookLoan> studentBookLoans = null;
            using (var dbModel = new LMSDataModelEntities())
            {               
                studentBookLoans = dbModel.StudentBookLoans.Where(loan => loan.ReturnDate == null || loan.DueDate >= DateTime.Now ).ToList();
                return studentBookLoans;
            }
        }

        public List<StudentBookLoan> GetCurrentStudentBookLoansByISBN(string isbn)
        {
            List<StudentBookLoan> studentBookLoans = null;
            using (var dbModel = new LMSDataModelEntities())
            {
                studentBookLoans = dbModel.StudentBookLoans.Where(loan => loan.book.Isbn.Equals(isbn, StringComparison.OrdinalIgnoreCase) && loan.ReturnDate == null).ToList();
                return studentBookLoans;
            }
        }

        public List<StudentBookLoan> GetCurrentStudentBookLoansByStudentReg(string studentRegId)
        {
            List<StudentBookLoan> studentBookLoans = null;
            using (var dbModel = new LMSDataModelEntities())
            {
                studentBookLoans = dbModel.StudentBookLoans.Where(loan => loan.student.RegistrationID.Equals(studentRegId, StringComparison.OrdinalIgnoreCase) && loan.ReturnDate == null).ToList();
                return studentBookLoans;
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