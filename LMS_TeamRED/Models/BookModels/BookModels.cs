using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LibraryManagementSystem.Models.Common;
using LibraryManagementSystem.Models.UserModels;

namespace LibraryManagementSystem.Models.BookModels
{
    public enum BookSearchType
    {
        Isbn = 1,
        Title = 2,
        Author = 3,
        Publisher = 4
    }
    

    public class SearchBookModel
    {
        [DisplayName("Search By")]
        public BookSearchType SearchType { get; set; }
        [DisplayName("Search String")]
        public String SearchString { get; set; }
    }


    public class LoanBookModel
    {
       [DisplayName("Book Title")]
       public String BookTitle { get; set; }
       public int BookId { get; set; }
       [DisplayName("Book ISBN")]
       public String BookIsbn { get; set; }
        
       [DisplayName("Student Reg. Number")]
       [Required]
       public String StudentReg { get; set; }
       public String IssuerId { get; set; }
       public bool IsInitial { get; set; }
    }


    public class ReturnBookModel
    {
        [DisplayName("Student Reg. Number")]
        [Required]
        public String StudentReg { get; set; }

        public bool ReturnSuccess { get; set; }
    }


}