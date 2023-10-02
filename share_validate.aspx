<%@ Page Language="C#" AutoEventWireup="true" CodeFile="share_validate.aspx.cs" Inherits="share_validate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Validate</title>
    <link href="css/home.css" type="text/css" rel="Stylesheet"/>
    <link href="css/homepage.css" rel="stylesheet" type="text/css" media="screen" />
</head>
<body>
<div id="container">
<div id="header" style="background-image:url('images/title1.jpg') " ></div>
    
<div style="background:#ccc; margin:100px auto; padding:60px; width:400px; padding-top:10px; line-height:2em; border:thin solid #333; text-align:center">
     <form id="validate_form" runat="server">
         <asp:Label id="validate_error" runat="server" Text="" ForeColor="red"></asp:Label><br/>
    Email Id:<asp:TextBox ID="userid" runat="server" placeholder="Email-id"></asp:TextBox><br />
    Authentication Code<asp:TextBox ID="random_number" runat="server" placeholder="Authentication code"></asp:TextBox><br />
    <asp:Button runat="server" ID="auth_submit" Text="Submit" onClick="validatehere"  />
    </form>
    </div>
    <div id="footer">Copyright &copy; Imagedit.com</div>
</div>
   
</body>
</html>
