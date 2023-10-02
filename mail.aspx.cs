using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.Net.Mail;


public partial class mail : System.Web.UI.Page
{
   private static Random random = new Random((int)DateTime.Now.Ticks);
        private string RandomString(int size)
    {
        StringBuilder builder = new StringBuilder();
        char ch;
        for (int i = 0; i < size; i++)
        {
            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));                 
            builder.Append(ch);
        }

        return builder.ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void sendmail(object sender, EventArgs e)
    {
       String name= (String)(Session["name"]);
        String email = receiver_id.Text;

        MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
        UTF8Encoding encoder = new UTF8Encoding();
        StringBuilder returnValue= new StringBuilder();

                 Byte[] originalemail;
                 Byte[] hashedemail;
                 originalemail = encoder.GetBytes(email);
                 hashedemail = md5Hasher.ComputeHash(originalemail);
                 
                 for (int i = 0; i < hashedemail.Length; i++)
                  {
                       returnValue.Append(hashedemail[i].ToString());
                   }

                 // return hexadecimal string
                  string hashemail =returnValue.ToString();



        string Rand1 = RandomString(20);
        String bodycode="<html><body>"+name+" has shared a photo with you.<br/> Visit thelink below to access the image.Use the given authentication code to view the image<br/>Authentication Code="+Rand1+"<br/><a  target=\"_blank\" href=\"http://imagedit.azurewebsites.net/share_validate.aspx\"> Click here to proceed</a></body></hmtl>";

            MailMessage mM = new MailMessage();
            mM.From = new MailAddress("noreplyimagedit@gmail.com");
            mM.To.Add(email);
            mM.Subject = "Welcome to Imagedit";
            mM.Body = bodycode;
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


            SqlConnection con = new System.Data.SqlClient.SqlConnection(@"Server=tcp:es7b5g9w5e.database.windows.net,1433;Database=imagedit;User ID=archana@es7b5g9w5e;Password=systemerrorA1;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
             con.Open();
             int id = Convert.ToInt16(Request.QueryString["id"]);
             int u_id=(int)(Session["id"]);
             String location;

             SqlCommand cmd1= new SqlCommand("select location from image where image_id=@image_id",con);
             
             cmd1.Parameters.AddWithValue("@image_id",id);
             location=Convert.ToString(cmd1.ExecuteScalar());
              
             SqlCommand cmd= new SqlCommand("insert into share(image,captcha,receiver,user_id)values(@image_id,@captcha,@email,@u_id)",con);
             cmd.Parameters.AddWithValue("@image_id",location);
             cmd.Parameters.AddWithValue("@captcha",Rand1);
             cmd.Parameters.AddWithValue("@email",hashemail);
             cmd.Parameters.AddWithValue("@u_id",u_id);
             int count=cmd.ExecuteNonQuery();


             if(count>0)
             {
            sC.Send(mM);
           
           Response.Redirect("share.aspx");
           // Label3.Text = "Mail Send Successfully";
            //Label3.ForeColor = Color.Green;
             }
             

    }

    protected string Getlocation()
        {

            var id = Request.QueryString["id"];
            String location;
           SqlConnection con = new System.Data.SqlClient.SqlConnection(@"Server=tcp:es7b5g9w5e.database.windows.net,1433;Database=imagedit;User ID=archana@es7b5g9w5e;Password=systemerrorA1;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
             con.Open();
             
             SqlCommand cmd= new SqlCommand("select location from image where image_id=@image_id",con);
             {
             cmd.Parameters.AddWithValue("@image_id",id);
             location=Convert.ToString(cmd.ExecuteScalar());
             }
            return location; // something like this
        }
}