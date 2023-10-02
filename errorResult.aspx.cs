using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public partial class errorResult : System.Web.UI.Page
{
     protected void Page_Load(object sender, EventArgs e)
    {
        var id = Request.QueryString["id"];
        if(id.Equals("1"))
        {
            message.Text="You might have provided wrong username or password please login again";
         }

        if(id.Equals("2"))
        {
           var id1 = Request.QueryString["id1"];
            message.Text=@"Yor are now registered with Imageid.Please confirm your email-id by visiting the mail sent to you<br/>Visit the link to authenticate your mobile number <a href='mobile_validate.aspx?id="+id1+"'>Mobile Verification</a>";
    
                 }
          if(id.Equals("3"))
        {
            message.Text="Some problem occurred while registering with Imagedit.Please try again";
         }
        }


          protected void Loginhere(object sender, EventArgs e)
    {
        String name=username.Text;
        String pwd= password.Text;
        String hashpwd;
        String dbpwd;
        //SqlDataReader sdr;


        MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
             UTF8Encoding encoder = new UTF8Encoding();
             Byte[] originalPassword;
             Byte[] hashedPassword;
             originalPassword = encoder.GetBytes(pwd);
            hashedPassword = md5Hasher.ComputeHash(originalPassword);
            //create new instance of StringBuilder to save hashed data
            StringBuilder returnValue = new StringBuilder();
            //loop for each byte and add it to StringBuilder
            for (int i = 0; i < hashedPassword.Length; i++)
             {
               returnValue.Append(hashedPassword[i].ToString());
              }
                    // return hexadecimal string
             hashpwd =returnValue.ToString();



          SqlConnection con = new System.Data.SqlClient.SqlConnection(@"Server=tcp:r9ifddd5jm.database.windows.net,1433;Database=imaged;User ID=archana@r9ifddd5jm;Password=systemerrorA1;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
          con.Open();
          
          SqlCommand cmd=new SqlCommand("Select password,name,id from client where email=@email and otp=1",con);
          {
              cmd.Parameters.AddWithValue("@email",name);
              SqlDataReader sdr= cmd.ExecuteReader();
              if(sdr.Read())
              {
                      dbpwd = sdr.GetString(0);
                      String fullname=sdr.GetString(1);
                      int id=sdr.GetInt32(2);
                     // Response.Write(dbpwd);
                     // Response.Write("hello" +dbpwd);
                      if(dbpwd==hashpwd)
                      {
                          Session["email"]=name;
                          Session["name"]=fullname;
                          Session["id"]=id;
                          Response.Redirect("gallery.aspx");
                  
                      }
                    else
                        {
                  
                            Response.Redirect("errorResult.aspx?id="+1);
                        }
              }
              else{
                  
                   Response.Redirect("errorResult.aspx?id="+1);
                   }
          }

         
          con.Close();

    }
        
    }
   
