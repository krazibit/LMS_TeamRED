<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LibraryManagementSystem.Models.BookModels.ReturnBookModel>" %>
<%@ Import Namespace="LibraryManagementSystemDAL.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Return Loaned Book
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentHeaderText" runat="server">
    <h1>Return Loaned Book</h1>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>
        <div class="searchBoxContainer"> 
               <% using (Html.BeginForm())
                  {%>
               <%: Html.LabelFor(model => model.StudentReg) %>
               <%: Html.TextBoxFor(model => model.StudentReg) %>
               <%: Html.ValidationMessageFor(model => model.StudentReg) %>
               <button class="submitBtn" type="submit" value="Search"> Get Books </button>
                <% } %>
        </div>
         <div class="searchContent">
             <%
           var loanList = (List<studentbookloan>)ViewData["LoanList"];
            if (loanList != null && loanList.Count >= 1)
            { %>
                <table class="searchResults">
                  <tbody>
                      <tr>
                          <th colspan="2">Book Information </th>
                          <th colspan="2">Loan Information</th>
                      </tr>
           <%
               int counter = 0;   
               foreach (var bookLoan in loanList)
               {
                   counter++;
           %>
              <%
                   var loanDetails = new studentbookloan
                                         {
                                             BookId =  bookLoan.BookId,
                                             DueDate = bookLoan.DueDate,
                                             IssueDate = bookLoan.IssueDate,
                                             IssuerID = bookLoan.IssuerID,
                                             ReturnDate = bookLoan.ReturnDate,
                                             StudentID = bookLoan.StudentID,
                                             book = bookLoan.book,
                                             id = bookLoan.id,
                                             student = bookLoan.student,
                                             systemuser = bookLoan.systemuser
                                         };
                   using (Html.BeginForm("ReturnProcessor", "ReturnBook", loanDetails, FormMethod.Post))
                 {%>
                <tr>
                    <td style="width: 40px;">
                      <%: counter %>  &nbsp;.
                    </td>
                    <td>
                         <%: bookLoan.book.Title%> 
                         <br/> <%: bookLoan.book.publisher.Name %> 
                         <br/> <%: bookLoan.book.Isbn %>
                    </td>
                    <td> <%: bookLoan.student.FirstName + " " + bookLoan.student.LastName %> &nbsp;&nbsp;&nbsp;&nbsp; 
                           <%: bookLoan.IssueDate.ToString("yyyy.MM.dd") + " - " + bookLoan.DueDate.ToString("yyyy.MM.dd")%>
                    </td> 
                    <td> <button type="submit" > Return </button> </td> 
                </tr>
                 <% }
               } %>
                 </tbody>
            </table>

            <%  } else { %>
          
             
                 <p class="noResult">No result matching search criteria 
                    <q class="noResult"> <%: Model.StudentReg%> </q>   
                  </p>
             <% } %>
  
         </div>  

        
      

    <% } %>

   

</asp:Content>


