<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mail.aspx.cs" Inherits="mail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="css/homepage.css" rel="stylesheet" type="text/css" media="screen" />
    <link type="text/css" href="css/menu.css" rel="stylesheet"  media="screen" />
    <link type="text/css" href="css/home.css" rel="stylesheet"  media="screen" />
    <title>Share Image</title>
</head>
<body>
    <div id="container">
<!--#include file="Header.aspx"-->
<div id="menu" style="background-image:url('images/b2.jpg'); height:790px">
 <!--#include file="menu.aspx"-->
</div>
<div style="padding:5px; width:auto; line-height:2em; border:thin solid #333; ">
<!--<p>${message}</p>-->
  <form runat="server" style="margin-left: 10px; padding: 50px ;text-align: center">
      
<h2 align="center">Enter receiver's mail id</h2>
<asp:Label id="error" runat="server" Text="" ForeColor="red"></asp:Label><br/>

Receiver's ID:
<asp:Textbox id="receiver_id" runat="server" placeholder="Email-id" required="required"/>
      <asp:button runat="server" id="loginbt" Text="Share" onClick="sendmail"  />
<br/>
Selected Image:
    <br/>
<img src="<%= Getlocation()%>" width="700px" height="500px" align="center" />
<br/>

          
</form>

</div> 
        
<div id="footer">Copyright &copy; Imagedit.com</div>
</div>
</body>
</html>