<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  LMS - Management Dashboard.
</asp:Content>

<asp:Content runat="server" ID="ContentHeaderText" ContentPlaceHolderID="ContentHeaderText">
    <h1> Management Dashboard</h1> 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <table id="dashBoardTable">
      <tr>
          
              <td>  
                    <a href="<% =Url.Action("Index", "Search")%>">
                        <img class="menuIcons" alt="Search Book" width="128" height="128" src="../../Assets/Images/Icons/searchBook.png"/>
                        <h4 class="menuIconText">Search Book</h4>
                    </a>
              </td> 
       
          <td>
              <a href="#">
                <img class="menuIcons" alt="Add Book" width="128" height="128" src="../../Assets/Images/Icons/addBook.png"/>
                <h4 class="menuIconText">Add Book</h4>
             </a>
          </td> 
          <td>
              <a href="<% =Url.Action("ReturnBook", "Home")%>">
                  <img class="menuIcons" alt="Return Book" width="128" height="128" src="../../Assets/Images/Icons/returnBook.png"/>
                  <h4 class="menuIconText">Return Book</h4>
              </a>
          </td> 
      </tr>
      <tr>
          <td>
              <a href="#"> 
                  <img class="menuIcons" alt="Student Status" width="142" height="127" src="../../Assets/Images/Icons/checkStudent.png"/>
                  <h4 class="menuIconText">Check<br/> Student Status</h4>
              </a>
          </td> 
          <td>
              <a href="<% =Url.Action("BorrowedBooks", "Search")%>"> 
                  <img class="menuIcons" alt="Borrowed Books" width="135" height="135" src="../../Assets/Images/Icons/borrowedBooks.png"/>
                  <h4 class="menuIconText">Borrowed Books</h4>
              </a>
          </td> 
          <td>
              <a href="<% =Url.Action("DueBooks", "Search")%>"> 
                  <img class="menuIcons" alt="Due Books" width="135" height="135" src="../../Assets/Images/Icons/dueBooks.png"/>
                  <h4 class="menuIconText">Due Books</h4>
              </a>
          </td> 
      </tr>
  </table>  
</asp:Content>
