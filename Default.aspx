<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Imagedit</title>
    <link href="css/homepage.css" rel="stylesheet" type="text/css" media="screen" />

    <!--loginbox-->
    <link href="css/login.css" rel="stylesheet" type="text/css" media="screen" />
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="js/lightb.js"></script>
</head>
<body>
<div id="container">
    <div id="header" style="background-image:url('images/title1.jpg')">
        <div id="toplink">
        <asp:HyperLink ID="HyperLink1" NavigateUrl="#login-box" class="login-window" runat="server">Sign In</asp:HyperLink>
        <span style='color:#ccc; font-weight:600;'>|</span>
        <asp:HyperLink ID="HyperLink2" NavigateUrl="register.aspx" runat="server">Register</asp:HyperLink>
        </div> 
    <div style="clear:both"></div>
    </div> 

    <div id="homepicture">
    <asp:Image ID="mainbg" runat="server" ImageUrl="images/main_bg.jpg" width="1365px" height="582px" /></div>
    <div id="footer">Copyright &copy; ImagedIt.com</div>
</div>
<div id="login-box" class="login-popup">
    <asp:HyperLink ID="HyperLink3" NavigateUrl="#" runat="server" class="close">
    <asp:Image ID="Image1" runat="server" ImageUrl="images/close_pop.png" class="btn_close" title="Close Window" alt="Close" /></asp:HyperLink>		
    <form id="loginform" class="signin"  runat="server">
        <fieldset class="textbox">
        <asp:label ID="Label1" class="username" runat="server">
        <span>Username</span>
        <asp:textbox id="username" name="username" type="text" autocomplete="on" placeholder="Email-id" runat="server" />
        </asp:label>
                
        <asp:label ID="Label2" class="password" runat="server">
        <span>Password</span>
        <asp:textbox TextMode="Password" runat="server" id="password" name="password" placeholder="Password" />
        </asp:label>
        <asp:label id="erorr_login" runat="server" forecolor="red"></asp:label>
                
        <asp:Button ID="Button1" Text="Login"  style="background:#bfbfbf;color:#000000;border-color:#212121;" onMouseOver="this.style.color = '#404040';" 
        onMouseOut="this.style.color = '#000000';" onFocusr="this.style.color = '#404040';" onBlur="this.style.color = '#000000';" 
        runat="server"  OnClick="Loginhere"/>
		<br/>
        
        <asp:HyperLink ID="HyperLink4" NavigateUrl="forgot_password.aspx" style="text-decoration:none; color:#000;" runat="server"><span>Forgot Password?</span></asp:HyperLink>
        </fieldset>
    </form>
</div>
</body>
</html>


