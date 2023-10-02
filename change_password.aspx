<%@ Page Language="C#" AutoEventWireup="true" CodeFile="change_password.aspx.cs" Inherits="change_password" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<link href="css/home.css" type="text/css" rel="Stylesheet"/>
<link href="css/homepage.css" type="text/css" rel="Stylesheet"/>
<title>Reset Password</title>
<script>
function validationCheck() {
               var summary = "";

               summary += isvalidConfirmpassword();


               if (summary != "") {
                   alert(summary);
                   return false;
               }
               else {
                   return true;
               }

           }


           function isvalidConfirmpassword() {
               var uidpwd;
               var uidcnmpwd;
               var tempcnmpwd = document.getElementById("<%=newpwd.ClientID %>");
               uidcnmpwd = tempcnmpwd.value;
               var temppwd = document.getElementById("<%=repwd.ClientID %>");
               uidpwd = temppwd.value;

               if (uidcnmpwd == "" || uidcnmpwd != uidpwd) {
                   return ("Please re-enter password to confrim" + "\n");
               }
               else {
                   return "";
               }
           }
        </script>   
</head>
<body>
<div id="container">
<!--#include file="Header.aspx"-->
    
<div style="background:#ccc; margin:100px auto; padding:60px; width:400px; padding-top:10px; line-height:2em; border:thin solid #333; text-align:center">
<!--<p>${message}</p>-->
<form runat="server" method="post">
    <asp:Label id="error" runat="server" Text="" ForeColor="red"></asp:Label><br/>
<h2 align="center">Change Password</h2>
<asp:table ID="Table1" runat="server">

<asp:tablerow>
<asp:tablecell>New Password:</asp:tablecell>
<asp:tablecell><asp:Textbox id="newpwd" TextMode="Password" runat="server" placeholder="Password" required="required" pattern="(?=.*\d)(?=.*[a-z])(?=.*[A-Z])\w{6,}" onchange="
this.setCustomValidity(this.validity.patternMismatch ? 'Password must contain at least 6 characters, including UPPER/lowercase and numbers' : '');
if(this.checkValidity()) form.pass.pattern = this.value;"/></asp:tablecell>
</asp:tablerow>

<asp:tablerow>
<asp:tablecell>Re-enter Password:</asp:tablecell>
 <asp:tablecell><asp:textbox id="repwd" TextMode="Password" runat="server" placeholder="Password" required="required" onchange="return validationCheck()"/></asp:tablecell>
</asp:tablerow>

</asp:table>
<asp:button runat="server" id="pwd" Text="Submit" onClick="resetpwd" /><div class="clear"></div>
</form>
    </div> 
<div id="footer">Copyright &copy; Imagedit.com</div>

    </div>
</body>
</html>
