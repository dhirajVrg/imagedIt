<%@ Page Language="C#" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="register" Debug="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Register New User</title>
    <link href="css/homepage.css" rel="stylesheet" type="text/css" media="screen" />

    <!-- registration form -->
    <link href="css/register.css" rel="stylesheet" type="text/css" media="screen" />


     <script type="text/javascript" >
             
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
               var tempcnmpwd = document.getElementById("<%=p1box.ClientID %>");
               uidcnmpwd = tempcnmpwd.value;
               var temppwd = document.getElementById("<%=p2box.ClientID %>");
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
<div id="container" style="background-image:url('images/b2.jpg')">
<div id="header" style="background-image:url('images/title1.jpg')" >
       
<div id="toplink"> 
<a href="Default.aspx">Welcome Page</a>
<a href="register.aspx">Register</a>
</div> 
<div style="clear:both"></div>
    </div> 

<div id="content">
    <div id="page_title">
        <div id="image" style="background-image:url('images/editpage_on.gif')"></div>
        <div id="text"><span>Create New User</span></div>
    </div>
    
    <form  id="reg_form" runat="server">
        <div id="information">
                 <asp:Label ID="emailerror" runat="server"  ForeColor="red"></asp:Label>
				<div id="title1" style="width:1365px; height:22px; float:left; background-color:#084B8A; color:#BDBDBD; font-size:18px"> 1. Personal Information </div>
        <div>
        <asp:table ID="Table1" runat="server" CellPadding="10" CellSpacing="10" style="float: left">
       
            <asp:tablerow>
            <asp:tablecell width="150px"> Name:</asp:tablecell>
            <asp:tablecell><asp:textbox type="text" id="username" class="tbox" maxlength="100" runat="server" required="required" placeholder="First Last"/></asp:tablecell>
            </asp:tablerow>

            <asp:tablerow>
            <asp:tablecell> Email_id:</asp:tablecell>
            <asp:tablecell><asp:Textbox type="email"  id="email" class="tbox" runat="server" required="required" maxlength="50" placeholder="example@example.com" />
            </asp:tablecell>
            </asp:tablerow>

            <asp:tablerow>
            <asp:tablecell> Mobile_Number:</asp:tablecell>
            <asp:tablecell><asp:Textbox id="mobnumber" class="tbox"  required="required" maxlength="10" placeholder="Mobile Number" pattern="(?=.*\d)\w{10}" onchange="this.setCustomValidity(this.validity.patternMismatch?'Please enter valid contact no.':'');if(this.checkValidity()) form.contact.pattern = this.value;" runat="server"/>
            
            </asp:tablecell>
            </asp:tablerow>
        </asp:table>
        </div>

        <div id="title2" style="width:1365px; height:22px; float:left; background-color:#084B8A; color:#BDBDBD; font-size:18px"> 2. Account Information </div>
        <div>
        <asp:table ID="Table2" runat="server" CellPadding="10" CellSpacing="10" style="float: left">
        <asp:tablerow>
        <asp:tablecell> Password:</asp:tablecell>
        <asp:tablecell><asp:Textbox runat="server" TextMode="password" id="p1box" class="tbox" required="required" placeholder="Enter Password" pattern="(?=.*\d)(?=.*[a-z])(?=.*[A-Z])\w{6,}" onchange="
this.setCustomValidity(this.validity.patternMismatch ? 'Password must contain at least 6 characters, including UPPER/lowercase and numbers' : '');
if(this.checkValidity()) form.pass.pattern = this.value;"/></asp:tablecell>
        </asp:tablerow>

         <asp:tablerow>
        <asp:tablecell> Re-Enter Password:</asp:tablecell>
        <asp:tablecell><asp:Textbox runat="server" TextMode="password" id="p2box" class="tbox" required="required" placeholder="Enter Password" onchange="return validationCheck()"/></asp:tablecell>
        </asp:tablerow>
       
        </asp:table>
        </div>

        <div id="title3" style="width:1365px; height:22px; float:left; background-color:#084B8A; color:#BDBDBD; font-size:18px"> 3. Submit </div>
        <div>
        <asp:table ID="Table3" runat="server" CellPadding="10" CellSpacing="10" style="float: left">
        <asp:tablerow>
        <asp:tablecell>Security Question:</asp:tablecell>
        <asp:tablecell height="2">
        <asp:DropDownList id="secure" runat="server" class="input" >
            <asp:ListItem>Who is your first teacher?</asp:ListItem>
            <asp:ListItem>which fruit do you like?</asp:ListItem>
            <asp:ListItem>Where do you live?</asp:ListItem>
            <asp:ListItem>What is your mother's name?</asp:ListItem>
            <asp:ListItem>What was the name of your first pet?</asp:ListItem>            
            </asp:DropDownList>
        </asp:tablecell>
        </asp:tablerow>
        <asp:tablerow>
        <asp:tablecell>Answer:</asp:tablecell>
        <asp:tablecell><asp:Textbox id="ans" runat="server" placeholder="answer" required="required" class="tbox"/></asp:tablecell>
        </asp:tablerow>
              

        <asp:tablerow>
        <asp:tablecell>
        <asp:Button ID="Button1"  runat="server" Text="Submit" OnClick="btnSubmit_Click" />
        <asp:Button ID="Button2"  runat="server" Text="Clear" OnClick="reset"  />
        </asp:tablecell>
        </asp:tablerow>
        </asp:table>
<br/>
</div>
</div>
</form>
</div>
<div id="footer">Copyright &copy; Imagedit.com</div>
</div>
</body>
</html>


