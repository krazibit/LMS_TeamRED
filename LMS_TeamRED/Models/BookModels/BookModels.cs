using System;
using System.ComponentModel;
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


}