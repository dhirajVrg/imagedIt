<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="share.aspx.cs" Inherits="share" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <title>Share</title>
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
        <p style="font-weight:600; font-size:15px;color: #084B8A;font-family:Verdana, Sans-serif; text-align:left"> 
    Click on image to start sharing</p>
    <div>

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
           
                     
                       while(sdr.Read())
                       {
                    
                           String location=sdr.GetString(0);
                           int image_id=sdr.GetInt32(1);
                                                     
                           %>

                            <a href="mail.aspx?id=<%=image_id%>"><div id="exhibit"><img src="<%=location%>" /></div></a>

                       
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

