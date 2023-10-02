<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="gallery.aspx.cs" Inherits="gallery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" href="css/exhibit.css" />    
    <link href="css/homepage.css" rel="stylesheet" type="text/css" media="screen" />
    <link type="text/css" href="css/menu.css" rel="stylesheet"  media="screen" />
    <link type="text/css" href="css/home.css" rel="stylesheet"  media="screen" />
    <link type="text/css" rel="stylesheet" href="css/colorbox.css" /> 
    <script  type="text/javascript" src="js/jquery.min.js"></script>
    <script  type="text/javascript" src="js/jquery.colorbox.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".exhibit").colorbox({ rel: 'exhibit', slideshow: true });
        });
    </script>
</head>
<body>
    <div id="container">
    <!--#include file="Header.aspx"-->
    <div id="menu" style="background-image:url('images/b2.jpg');">
    <!--#include file="menu.aspx"-->
    </div>
    <div>

 <%
int id=(int)Session["id"];

SqlDataReader sdr;
SqlConnection con = new System.Data.SqlClient.SqlConnection(@"Server=tcp:es7b5g9w5e.database.windows.net,1433;Database=imagedit;User ID=archana@es7b5g9w5e;Password=systemerrorA1;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
   {
       
        
           SqlCommand cmd1 = new SqlCommand("select location from image where user_id=@id", con);
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
                           %>

                            <a class="exhibit" href="<%=location%>"><div id="exhibit"><img src="<%=location%>" /></div></a>

                       
                        <%
                           }
               }
               con.Close();
             }   
         }
   
   

    


%>
      
      
    </div>
    <div id="footer">Copyright &copy; Imagedit.com</div> 
    </div>
</body>
</html>

