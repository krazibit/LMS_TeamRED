using LibraryManagementSystem.Models.Common;
using System;

namespace LibraryManagementSystem.Models.StudentModels
{
    public enum StudentSearchType
    {
        ByRegNo,
        ByEmail,
        ByName,
        ByTelephone
    }

    public class StudentModel
    {
        public String RegNo { get; private set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String Telephone { get; set; }
        public String Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime EnrolmentDate { get; set; }
        public bool CurrentlyRegistered { get; set; }
        public Sex Sex { get; set; }
        public Department Department { get; set; }   
    }
    
    public class FindStudent
    {
        public StudentSearchType? SearchType { get; set; }
        public String SearchString { get; set; }
    }

}