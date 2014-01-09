<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<IEnumerable<LibraryManagementSystem.Models.BookModels.BookModel>>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Search
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentHeaderText" runat="server">
    <h1> Search Books </h1>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%
       
        if (Model!=null)
        { 
      foreach (var bookModel in Model)
            {
   %>
             <h2>Search is not Empty!</h2>
        <%   } } else { %>
          
             <h2>Search is not Empty!</h2>
     
       <% } %>
       
</asp:Content>


