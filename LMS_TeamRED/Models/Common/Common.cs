using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.Models.Common
{
   public enum Sex
   {
       Male,
       Female
   }

   public class Department
   {
       public long Id { get; private set; }
       public String Name { get; set; }
   }
}