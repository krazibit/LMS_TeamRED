using System;
using LibraryManagementSystem.Models.Common;

namespace LibraryManagementSystem.Models.UserModels
{
    public enum UserRole
    {
        Normal,
        Administrator
    }

    public class User
    {
        public long Id { get; private set; }
        public UserRole Role { get; set; }
        public String Username { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastLogin { get; set; }
        public Sex Sex { get; set; }
    }
}