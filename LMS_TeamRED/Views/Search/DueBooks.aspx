<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IList<LibraryManagementSystemDAL.Data.studentbookloan>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	LMS - Due Unreturned Books
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
     <div class="searchContent">
    <% if (Model == null || Model.Count < 1)
       { %>
       <p class="noResult">
           There are currently no  <q class="noResult"> due unreturned books  </q>  in the system.   
       </p>   
       <% }
       else
       { %>
                  <table class="bookList"> 
                    <tbody>
                        <tr>
                            <th >No.</th>
                            <th>Book Information</th>
                            <th>Holder's Information</th>
                            <th>Loan Date</th>
                            <th>Days Due</th>
                        </tr>
                  <% var counter = 1;
                     foreach (var loanInfo in Model)
                     { %>
                        <tr >
                            <td>
                                <%: counter %> .
                            </td>
                            <td>
                                <%: loanInfo.book.Title %> &nbsp;&nbsp; <%: loanInfo.book.PublicationDate.ToString("yyyy.MM.dd") %> <br/>
                                <%: loanInfo.book.Isbn %>
                            </td>
                            <td>
                                <%: loanInfo.student.FirstName + " " + loanInfo.student.LastName %> <br/>
                                <%: loanInfo.student.RegistrationID %>
                            </td>
                            <td>
                                <%: loanInfo.IssueDate.ToString("yyyy.MM.dd") %>
                            </td>
                            <td>
                                <%: Math.Ceiling((DateTime.Now - loanInfo.DueDate).TotalDays) %>
                            </td>
                        </tr>  
                  <% counter++;
                     } %>
                     
                     </tbody>
                </table>
                <% }%>
                </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentHeaderText" runat="server">
   <h1>Due Unreturned Books</h1>
</asp:Content>




