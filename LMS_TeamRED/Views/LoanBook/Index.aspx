<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LibraryManagementSystem.Models.BookModels.LoanBookModel>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Process Book [<%: Model.BookTitle %>] Loan
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentHeaderText" runat="server">
    <h1>Process Book Loan </h1>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <script type="text/javascript">
        var bSuccess = "<%=  ViewData["LoanSuccess"] ==null ? "0" : (ViewData["LoanSuccess"]).ToString() %>";
        var bInitCall = "<%= ViewData["InitialCall"] ==null ? "0" : (ViewData["InitialCall"]).ToString()  %>";
        $(document).ready(function () {
            
            if(bInitCall==="0") 
            {
                if (bSuccess==="1") 
                {
                    alert("Book Successfully Loaned!");
                    window.location.href = "/Home/Index";
                } 
                else 
                {
                    alert("Student Registration Number Is not Valid!");
                }
            }
        });
    </script>

    <div id="processLoan">
    <% using (Html.BeginForm("Index", "LoanBook",FormMethod.Post))
       {%>
        <%: Html.ValidationSummary(true) %>
        <% object tagDisabled = new {disabled = "disabled"}; %>
        <fieldset>
            <div class="display-label" >
                <%: Html.LabelFor(model => model.BookTitle)%>
            </div>
            <div class="display-field">
                <%: Html.TextBoxFor(model => model.BookTitle, tagDisabled)%>
                <%: Html.HiddenFor(model => model.BookTitle)%>
                <%: Html.ValidationMessageFor(model => model.BookTitle) %>
            </div>
            
            <div class="display-label">
                <%: Html.LabelFor(model => model.BookIsbn) %>
            </div>
            <div class="display-field">
                <%: Html.TextBoxFor(model => model.BookIsbn, tagDisabled)%>
                <%: Html.HiddenFor(model => model.BookIsbn)%>
                <%: Html.ValidationMessageFor(model => model.BookIsbn) %>
            </div>
            
            <div class="display-label">
                <%: Html.LabelFor(model => model.StudentReg) %>
            </div>
            <div class="display-field">
                <%: Html.TextBoxFor(model => model.StudentReg) %>
                <%: Html.ValidationMessageFor(model => model.StudentReg) %>
            </div>
            <%: Html.HiddenFor(model => model.BookId) %>
             <%: Html.HiddenFor(model => model.IsInitial)%>
            <p>
                <input type="submit" value="Loan" />
            </p>
        </fieldset>

    <% } %>
    </div>
</asp:Content>




