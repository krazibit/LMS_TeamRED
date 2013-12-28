<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Search
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentHeaderText" runat="server">
    <h1> Search Books </h1>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% 
        if (ViewData["IsEmpty"].Equals("2"))
        { 
     %>
        <h2>Search is Empty!</h2>
      <%
        } else {
      %>
         <h2>Search is not Empty!</h2>
      <% } %>
       
</asp:Content>


