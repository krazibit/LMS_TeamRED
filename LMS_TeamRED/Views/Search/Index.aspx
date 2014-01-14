<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<LibraryManagementSystem.Models.BookModels.SearchBookModel>" %>
<%@ Import Namespace="LibraryManagementSystem.Models.BookModels" %>
<%@ Import Namespace="LibraryManagementSystemDAL.Data" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Search
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentHeaderText" runat="server">
    <h1> Search Books </h1>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="searchBoxContainer"> 
           <% using (Html.BeginForm())
              {%>
           <%: Html.LabelFor(m => m.SearchString) %>
           <%: Html.TextBoxFor(m => m.SearchString) %>
            Search by
           <%: Html.DropDownListFor(m => m.SearchType, new SelectList(Enum.GetValues(typeof(BookSearchType))))%>
            <button class="submitBtn" type="submit" value="Search"> Search </button>
            <% } %>
    </div>
    
    <hr class="searchDivider"/>
    
    <div class="searchContent">
        <%
            var books = ViewData["BookMap"] as  Dictionary<book, int>;
            if (books != null && books.Count >= 1)
            { %>
                <table class="searchResults">
                  <tbody>
                      <tr>
                          <th>No. </th>
                          <th>Title/Author(s)</th>
                          <th> Publisher </th>
                          <th> Available Copies </th>
                          <th> </th>
                      </tr>
           <%
               int counter = 0;   
               foreach (var bookModel in books)
               {
                   counter++;
           %>
              <%
                   var loanBookModel = new LoanBookModel
                                           {
                                               BookId = bookModel.Key.Id,
                                               BookIsbn = bookModel.Key.Isbn,
                                               BookTitle = bookModel.Key.Title,
                                               IsInitial = true
                                           };
                   using (Html.BeginForm("Index", "LoanBook", loanBookModel, FormMethod.Post))
                 {%>
                <tr>
                    <td>
                      <%: counter %>
                    </td>
                    <td>
                         <%: bookModel.Key.Title %> <br/> TODO: GetAuthors 
                    </td>
                    <td> <%: bookModel.Key.publisher.Name %></td> 
                    <td> <%: bookModel.Value %></td>
                    <td> <button type="submit"  <% if (bookModel.Value < 1)
                                                   { %> disabled = "disabled" <% } %>> Loan </button> </td> 
                </tr>
                 <% }
               } %>
                 </tbody>
            </table>

            <%  } else { %>
          
              <% var queried = ViewData["Queried"];
                  if((bool)queried == true)
                  { %>
                 <p class="noResult">No result matching search criteria 
                    <q class="noResult"> <%: Model.SearchString%> </q>   using 
                    <q class="noResult"> <%: Enum.GetName(typeof(BookSearchType), Model.SearchType)%> </q>
                  </p>
             <% } %>
           <% } %>
       </div>
</asp:Content>


