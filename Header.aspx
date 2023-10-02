<div id="header" style="background-image:url('images/title1.jpg') " >

 <div id="toplink">


<%

Response.Cache.SetCacheability(HttpCacheability.NoCache);
Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
      Response.Cache.SetNoStore();
String name= (String)(Session["name"]);
String email=(String)(Session["email"]);
if(name==null)
Response.Redirect("Default.aspx");



else
{
%>
 <span style="color:#ccc;font-family:Verdana, Sans-serif;font-weight:600;"><%=name%></span>
<span style='color:#ccc; font-weight:600;'>|</span><a href="change_password.aspx">Change Password</a>
<span style='color:#ccc; font-weight:600;'>|</span><a href="logout.aspx">Logout</a>

<%}%>

</div>
<div style="clear:both"></div></div>
