using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Security.Cryptography;


public partial class validate : System.Web.UI.Page
{
     protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void validateme(object sender, EventArgs e)
    {
        string name=userid.Text;
        string random=random_number.Text;
        string hashemail;
  

        MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
        UTF8Encoding encoder = new UTF8Encoding();
         StringBuilder returnValue = new StringBuilder();


             Byte[] originalemail;
             Byte[] hashedemail;
            originalemail = encoder.GetBytes(name);
           hashedemail = md5Hasher.ComputeHash(originalemail);

           returnValue = new StringBuilder();

         //loop for each byte and add it to StringBuilder
         for (int i = 0; i < hashedemail.Length; i++)
          {
               returnValue.Append(hashedemail[i].ToString());
           }

         // return hexadecimal string
          hashemail =returnValue.ToString();

          
        SqlConnection con = new SqlConnection(@"Server=tcp:es7b5g9w5e.database.windows.net,1433;Database=imagedit;User ID=archana@es7b5g9w5e;Password=systemerrorA1;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
        con.Open();
           //Response.Write(con);
      
                   int count;
                          SqlCommand cmd1=new SqlCommand("update client set otp=1 where email like @email and captcha like @captcha ",con);
                          {
                               cmd1.Parameters.AddWithValue("@email",hashemail);
                               cmd1.Parameters.AddWithValue("@captcha",random);
                               count =cmd1.ExecuteNonQuery();
                               
                               String path="/Client/"+hashemail;
                               Directory.CreateDirectory(Server.MapPath(path));
                          }
                 
                              if(count>0)
                              {
                                  
                                  //String path="/Client/"+name;
                                  //Directory.CreateDirectory(Server.MapPath(path));
                                  Response.Redirect("default.aspx");

                              }
                              else
                              {
                                   validate_error.Text="There seems some problem.The data you enter is not correct";
                                   userid.Text="";
                                   random_number.Text="";
                                  //Server.Transfer("validate.aspx");
                 
                              }
     
            con.Close();
            }
    }