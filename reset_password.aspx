<%@ Page Language="C#" AutoEventWireup="true" CodeFile="reset_password.aspx.cs" Inherits="reset_password" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>Reset Password</title>
    <title>Forgot Password</title>
    <link href="css/home.css" type="text/css" rel="Stylesheet"/>
    <link href="css/homepage.css" rel="stylesheet" type="text/css" media="screen" />
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
<div id="header" style="background-image:url('images/title1.jpg') " ></div>
<div style="background:#ccc; margin:150px auto; padding:60px; width:400px; padding-top:30px; line-height:2em; border:thin solid #333; ">
<!--<p>${message}</p>-->
<form runat="server" method="post">
    <asp:Label id="error" runat="server" Text="" ForeColor="red"></asp:Label><br/>
<h2 align="center">Reset Password</h2>
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
