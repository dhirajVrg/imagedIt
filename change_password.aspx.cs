using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;


public partial class change_password : System.Web.UI.Page
{
   protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void resetpwd(object sender, EventArgs e)
    {
       String email=(String)(Session["email"]);
       String hashpwd;
        
        //Response.Write(email);
         String pwd=newpwd.Text;
        String repqd=repwd.Text;
        //Response.Write(email);
     if(pwd==repqd)
        {
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

          SqlConnection con = new System.Data.SqlClient.SqlConnection(@"Server=tcp:es7b5g9w5e.database.windows.net,1433;Database=imagedit;User ID=archana@es7b5g9w5e;Password=systemerrorA1;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
          SqlCommand cmd=new SqlCommand("update client set password=@pwd where email=@email",con);
           cmd.Parameters.AddWithValue("@pwd",hashpwd);
          cmd.Parameters.AddWithValue("@email",email);
          con.Open();
          int count=cmd.ExecuteNonQuery();
          con.Close();
          if(count>0)
                  {
                  Response.Redirect("Default.aspx");
                  }
         else
                {
                    error.Text="Error";
                    Server.Transfer("reset_password.aspx");
                }

          
        }
        else
        {
            error.Text="Error";
            Server.Transfer("reset_password.aspx");
        }
        
        }
    
}