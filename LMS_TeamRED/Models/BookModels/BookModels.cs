using System;
using LibraryManagementSystem.Models.Common;
using LibraryManagementSystem.Models.UserModels;

namespace LibraryManagementSystem.Models.BookModels
{
    public enum BookSearchType
    {
        ByIsbn,
        ByTitle,
        ByAuthor,
        ByPublisher
    }
    
    public class BookModel
    {
        public long BookId { get; private set; }
        public String Isbn { get; private set; }
        public String Title { get; private set; }
        public DateTime PublicationYear { get; private set; }
        public DateTime InsertionDate { get; private set; }
        public bool Available { get; set; }
        public Publisher Publisher { get; set; }
        public Department Department { get; set; }
        public User AddedBy { get; set; }
    }

    public class Author
    {
        public long Id { get; private set; }
        public String Salutation { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String MiddleName { get; set; }
    }

    public class Publisher
    {
        public long Id { get; private set; }
        public String Name { get; set; }
        public string Telephone { get; set; }
        public String Email { get; set; }
        public String Address { get; set; }
    }

    public class SearchBookModel
    {
        public BookSearchType SearchType { get; set; }
        public String SearchString { get; set; }
    }


}