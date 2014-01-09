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