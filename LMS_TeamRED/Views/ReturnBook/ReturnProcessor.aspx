<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<LibraryManagementSystem.Models.BookModels.ReturnBookModel>" %>
<%@ Import Namespace="System.Globalization" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ReturnProcessor</title>
     <script src="../../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
     
</head>
<body>
    <script type="text/javascript">
        var bSuccess = ("<%=Model.ReturnSuccess.ToString(CultureInfo.InvariantCulture).ToLower() %>");
       $(document).ready(function () {
                if (bSuccess=="true") 
                {
                    alert("Book Return Successful!");
                    window.location.href = "<%:Url.Action("Index", "ReturnBook", Model)%>" ;
                } 
                else 
                {
                    alert("Book Return Unsuccessful!");
                    window.location.href = "<%: Url.Action("Index", "ReturnBook", Model)%>" ;
                }
        });
         </script>
        
   
</body>
</html>
