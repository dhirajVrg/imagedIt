using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public partial class forgot_password : System.Web.UI.Page
{
   protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void changepwd(object sender, EventArgs e)
    {
        String id=name.Text;
        String Question=secure.SelectedItem.Text;
        String Answer=ans.Text;
        SqlDataReader sdr;
        String hashemail;
        //Response.Write(id);
        // Response.Write(Question);

         
        MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
        UTF8Encoding encoder = new UTF8Encoding();
         Byte[] originalemail;
         Byte[] hashedemail;
            originalemail = encoder.GetBytes(id);
           hashedemail = md5Hasher.ComputeHash(originalemail);

          StringBuilder returnValue = new StringBuilder();

         //loop for each byte and add it to StringBuilder
         for (int i = 0; i < hashedemail.Length; i++)
          {
               returnValue.Append(hashedemail[i].ToString());
           }

         // return hexadecimal string
          hashemail =returnValue.ToString();

                 returnValue.Clear();
                 Byte[] originalquestion;
                 Byte[] hashedquestion;
                 originalquestion = encoder.GetBytes(Question);
                 hashedquestion = md5Hasher.ComputeHash(originalquestion);
                

                  for (int i = 0; i < hashedquestion.Length; i++)
                  {
                       returnValue.Append(hashedquestion[i].ToString());
                   }

                 // return hexadecimal string
                  string hashquestion =returnValue.ToString();
                   returnValue.Clear();


                 Byte[] originalanswer;
                 Byte[] hashedanswer;
                 originalanswer = encoder.GetBytes(Answer);
                 hashedanswer = md5Hasher.ComputeHash(originalanswer);
                 

                  for (int i = 0; i < hashedanswer.Length; i++)
                  {
                       returnValue.Append(hashedanswer[i].ToString());
                   }

                 // return hexadecimal string
                  string hashanswer =returnValue.ToString();
                   returnValue.Clear();





            
       SqlConnection con = new System.Data.SqlClient.SqlConnection(@"Server=tcp:es7b5g9w5e.database.windows.net,1433;Database=imagedit;User ID=archana@es7b5g9w5e;Password=systemerrorA1;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
        con.Open();
        SqlCommand cmd= new SqlCommand("select security,answer from client where email=@email",con);
        {   
            cmd.Parameters.AddWithValue("@email",hashemail);
           using(sdr= cmd.ExecuteReader())
           { 
           //Response.Write(Question);
           while(sdr.Read())
           {
               //Response.Write(Answer);
               String secure1=sdr.GetString(0);
               String ans1=sdr.GetString(1);
               //Response.Write(secure1);
               //Response.Write(ans1);
               if(secure1==hashquestion && ans1== hashanswer)
               {
               Response.Redirect("reset_password.aspx?id="+hashemail);
               }

               else
               {
                   Server.Transfer("forgot_password.aspx");
               }
           }

        }}



    }
}