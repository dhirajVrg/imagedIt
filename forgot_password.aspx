<%@ Page Language="C#" AutoEventWireup="true" CodeFile="forgot_password.aspx.cs" Inherits="forgot_password" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Forgot Password</title>
    <link href="css/home.css" type="text/css" rel="Stylesheet"/>
    <link href="css/homepage.css" rel="stylesheet" type="text/css" media="screen" />
</head>
    <body>
<div id="container">
<div id="header" style="background-image:url('images/title1.jpg') " ></div>

<div style="background:#ccc; margin:150px auto; padding:60px; width:400px; padding-top:30px; line-height:2em; border:thin solid #333; ">
<!--<p>${message}</p>-->
<form runat="server" method="post">
<h2 align="center">Forgot Password</h2>
        <asp:Label id="error" runat="server" Text="" ForeColor="red"></asp:Label><br/>
<asp:table runat="server">
<asp:tablerow>
<asp:tablecell>ID:</asp:tablecell>
<asp:tablecell><asp:Textbox id="name" runat="server" placeholder="Email-id" required="required"/></asp:tablecell>
</asp:tablerow>
<asp:tablerow>
<asp:tablecell>Security Question:</asp:tablecell>
<asp:tablecell><asp:DropDownList id="secure" runat="server" class="input" >
            <asp:ListItem>Who is your first teacher?</asp:ListItem>
            <asp:ListItem>which fruit do you like?</asp:ListItem>
            <asp:ListItem>Where do you live?</asp:ListItem>
            <asp:ListItem>What is your mother's name?</asp:ListItem>
            <asp:ListItem>What was the name of your first pet?</asp:ListItem>            
            </asp:DropDownList></asp:tablecell>
</asp:tablerow>
<asp:tablerow>
<asp:tablecell>Answer:</asp:tablecell>
<asp:tablecell><asp:Textbox runat="server" id="ans" placeholder="Answer"/></asp:tablecell>
</asp:tablerow>
</asp:table>
<asp:button runat="server" id="loginbt" Text="Submit" onClick="changepwd" /><div class="clear"></div>
</form>
 </div>  
<div id="footer">Copyright &copy; Imagedit.com</div>
  
</div>
</body>
</html>