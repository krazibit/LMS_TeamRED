<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<LibraryManagementSystem.Models.BookModels.SearchBookModel>" %>
<%@ Import Namespace="LibraryManagementSystem.Data" %>
<%@ Import Namespace="LibraryManagementSystem.Models.BookModels" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Search
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentHeaderText" runat="server">
    <h1> Search Books </h1>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="searchBoxContainer"> 
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
    
    <div id="searchContent">
        <%
            var books = ViewData["BooksMap"] as  Dictionary<Book, int>;
            if (books != null && books.Count >= 1)
            {
                foreach (var bookModel in books)
                {
       %>
                 <h2>Search is not Empty!</h2>
            <%   } } else { %>
          
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


