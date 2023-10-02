<%@ Page Language="C#" AutoEventWireup="true" CodeFile="uploadimage.aspx.cs" Inherits="uploadimage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upload Image</title>
    <link href="css/homepage.css" rel="stylesheet" type="text/css" media="screen" />
    <link type="text/css" href="css/menu.css" rel="stylesheet"  media="screen" />
    <link type="text/css" href="css/home.css" rel="stylesheet"  media="screen" />
    <script type ="text/javascript">
        var validFilesTypes = ["bmp", "gif", "png", "jpg", "jpeg","JPG","JPEG"];
        function ValidateFile() {
        var file = document.getElementById("<%=FileUpload1.ClientID%>");
        var label = document.getElementById("<%=Label1.ClientID%>");
        var path = file.value;
        var ext = path.substring(path.lastIndexOf(".") + 1, path.length).toLowerCase();
        var isValidFile = false;
        for (var i = 0; i < validFilesTypes.length; i++) {
            if (ext == validFilesTypes[i]) {
                isValidFile = true;
                break;
            }
        }
        if (!isValidFile) {
            label.style.color = "red";
            label.innerHTML = "Invalid File. Please upload a File with" +
         " extension:\n\n" + validFilesTypes.join(", ");
        }
        return isValidFile;
    }
</script> 
</head>
<body>
<div id="container">
    <!--#include file="Header.aspx"-->
    <div id="menu" style="background-image:url('images/b2.jpg');">
    <!--#include file="menu.aspx"-->
    </div>
    <div style="float:left;">
    <form id="form1" runat="server" style="text-align:center" >
    <div style="margin:100px; background-color:#236692; padding:50px;">
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:Button ID="btnUpload" runat="server" Text="Upload"  
           OnClientClick = "return ValidateFile()"  OnClick="btnUpload_Click"  />
        <br />
        <asp:Label ID="Label1" runat="server" Text="" />
    </div>
    </form>
    </div>
    <div id="footer">Copyright &copy; Imagedit.com</div> 
</div>
</body>
</html>

