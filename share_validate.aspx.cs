using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.Net.Mail;
public partial class share_validate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

      protected void validatehere(object sender, EventArgs e)
    {
        string name = userid.Text;
        string random = random_number.Text;


               MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
                UTF8Encoding encoder = new UTF8Encoding();
                StringBuilder returnValue= new StringBuilder();

                 Byte[] originalemail;
                 Byte[] hashedemail;
                 originalemail = encoder.GetBytes(name);
                 hashedemail = md5Hasher.ComputeHash(originalemail);
                 
                 for (int i = 0; i < hashedemail.Length; i++)
                  {
                       returnValue.Append(hashedemail[i].ToString());
                   }

                 // return hexadecimal string
                  string hashemail =returnValue.ToString();
                  

        string location;
        //string captcha;
       SqlConnection con = new System.Data.SqlClient.SqlConnection(@"Server=tcp:es7b5g9w5e.database.windows.net,1433;Database=imagedit;User ID=archana@es7b5g9w5e;Password=systemerrorA1;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
        con.Open();
        SqlCommand cmd=new SqlCommand("select image from share where receiver like @receiver and captcha like @captcha",con);
        cmd.Parameters.AddWithValue("@receiver",hashemail);
        cmd.Parameters.AddWithValue("@captcha",random);
        location = Convert.ToString(cmd.ExecuteScalar());
        if (!(string.IsNullOrEmpty(location)))
        {

            Server.Transfer("share_View.aspx?location=" + location);
        }
        else
        {
            Response.Write("hello1");
            MailMessage mM = new MailMessage();
            mM.From = new MailAddress("noreplyimagedit@gmail.com");
            mM.To.Add("noreplyimagedit@gmail.com");
            mM.Subject = "Welcome to Imagedit";
            mM.Body = "Someone tried to access shared image of"+name;
            mM.IsBodyHtml = true;
           /* if (FileUpload1.HasFile)
            {

                mM.Attachments.Add(new Attachment(FileUpload1.PostedFile.InputStream, FileUpload1.FileName));

            }*/
            mM.Priority = MailPriority.High;
            SmtpClient sC = new SmtpClient("smtp.gmail.com");
            sC.Port = 587;
            sC.EnableSsl = true;
            sC.UseDefaultCredentials = true;
            sC.Credentials = new NetworkCredential("noreplyimagedit@gmail.com", "admin@123456789");
            sC.Send(mM);
               validate_error.Text="There seems some problem.The data you enter is not correct";
           userid.Text="";
           random_number.Text="";
            //Response.Redirect("share_validate.aspx");
        }
    }
}