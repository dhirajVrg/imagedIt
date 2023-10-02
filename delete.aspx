<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="delete.aspx.cs" Inherits="delete" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Delete Image</title>
    <link href="css/login.css" rel="stylesheet" type="text/css" media="screen" />   
    <link href="css/homepage.css" rel="stylesheet" type="text/css" media="screen" />
    <link href="css/menu.css" rel="stylesheet" type="text/css" media="screen" />
    <link type="text/css" href="css/home.css" rel="stylesheet"  media="screen" />
    <link rel="stylesheet" href="css/exhibit.css" />
    <style type="text/css">
.deletelink
{
font-family: Verdana Sans-Serif Serif;
background-color: #cbcbcb;
color: #084B8A;
font-size:8;
text-decoration:none;
padding:1px 60px 1px 60px;
margin:10px;
border-radius:5px;
border-width:thin;
-webkit-box-shadow: 2px 2px 4px #888;

}
.deletelink:hover {background-color: #cbcbcb;
color: #585858;}

</style>

</head>
<body>
<div id="container">
    <!--#include file="Header.aspx"-->
    <div id="menu" style="background-image:url('images/b2.jpg');">
    <!--#include file="menu.aspx"-->
    </div>
    <div>
        <form id="delete_form" runat="server">
        <%
int id=(int)Session["id"];

SqlDataReader sdr;
 SqlConnection con = new System.Data.SqlClient.SqlConnection(@"Server=tcp:es7b5g9w5e.database.windows.net,1433;Database=imagedit;User ID=archana@es7b5g9w5e;Password=systemerrorA1;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
   {
       
        
           SqlCommand cmd1 = new SqlCommand("select location,image_id from image where user_id=@id", con);
           {
               con.Open();
              cmd1.Parameters.AddWithValue("@id",id);
              using(sdr= cmd1.ExecuteReader())
              {
           
                       //Response.Write(Question);
                       while(sdr.Read())
                       {
                           
                           //Response.Write(Answer);
                           String location=sdr.GetString(0);
                            int image_id=sdr.GetInt32(1);
                           %>
         
            <div id="exhibit"><img src="<%=location%>" /><a  class="deletelink" href="deleteImage.aspx?id=<%=image_id%>">Delete</a></div>
    
<%
                           }
               }
             }   
         }

%>
   
    
    </form>
    </div>
    <div id="footer">Copyright &copy; Imagedit.com</div> 
</div>
</body>
</html>
