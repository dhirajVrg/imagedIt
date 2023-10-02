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
using System.Net;
using System.Net.Mail;
using ASPSnippets.SmsAPI;

public partial class _Default : System.Web.UI.Page
{
protected void Page_Load(object sender, EventArgs e)
    {
        //username.Text="";
        //password.Text="";
    }



    private string Decrypt(string strEncrypted, string strKey)
        {
            try
            {
                TripleDESCryptoServiceProvider objDESCrypto =
                    new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
                byte[] byteHash, byteBuff;
                string strTempKey = strKey;
                byteHash = objHashMD5.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(strTempKey));
                objHashMD5 = null;
                objDESCrypto.Key = byteHash;
                objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
                byteBuff = Convert.FromBase64String(strEncrypted);
                string strDecrypted = System.Text.ASCIIEncoding.ASCII.GetString
                (objDESCrypto.CreateDecryptor().TransformFinalBlock
                (byteBuff, 0, byteBuff.Length));
                objDESCrypto = null;
                return strDecrypted;
            }
            catch (Exception ex)
            {
                return "Wrong Input. " + ex.Message;
            }
        }


        protected void Loginhere(object sender, EventArgs e)
    {
        String name=username.Text;
        String pwd= password.Text;
        String hashpwd;
         String hashemail;
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



          SqlConnection con = new System.Data.SqlClient.SqlConnection(@"Server=tcp:es7b5g9w5e.database.windows.net,1433;Database=imagedit;User ID=archana@es7b5g9w5e;Password=systemerrorA1;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
          con.Open();
          
          SqlCommand cmd=new SqlCommand("Select password,name,id from client where email=@email and otp=1 and motp=1",con);
          {
              cmd.Parameters.AddWithValue("@email", hashemail);
              SqlDataReader sdr= cmd.ExecuteReader();
              if (sdr.Read())
              {
                  dbpwd = sdr.GetString(0);
                  String fullname = sdr.GetString(1);
                  int id = sdr.GetInt32(2);
               
                  String userFullname="";

                  
            try
            {
                userFullname = Decrypt(fullname,hashemail );
            }
            catch (Exception ex)
            {
                userFullname= "Wrong Input. " + ex.Message;
            }


                  if (dbpwd == hashpwd)
                  {
                      Session["email"] = hashemail;
                      Session["name"] = userFullname;
                      Session["id"] = id;
                       sdr.Close();
                      
                      SqlCommand cmd1 = new SqlCommand("Update client set attempt=0 where email=@email  ", con);
                      {
                          cmd1.Parameters.AddWithValue("@email", hashemail);
                          cmd1.ExecuteScalar();
                      }
                      Response.Redirect("gallery.aspx");
                     
                  }

                  else
                  {

                     sdr.Close();
                      SqlCommand cmd2 = new SqlCommand("Update client set attempt=attempt+1 where email=@email ", con);
                      {
                          cmd2.Parameters.AddWithValue("@email", hashemail);
                          cmd2.ExecuteScalar();
                      }
                      SqlCommand cmd1 = new SqlCommand("Select attempt from client where email=@email", con);
                      {
                          cmd1.Parameters.AddWithValue("@email", hashemail);
                          
                          
                              var dbattempt = (Int32)cmd1.ExecuteScalar();
                              if (dbattempt > 3)
                              {
                                  SqlCommand cmd3 = new SqlCommand("Select mobile from client where email=@email", con);
                      
                                     cmd3.Parameters.AddWithValue("@email", hashemail);
                                     var mobile = (String) cmd3.ExecuteScalar();
                                     String decrypt_mobile= Decrypt(mobile,hashemail );

                                  try
                                  {
                                      String body="Suspicious activity has been detected with respect to your account on Imagedit.com . We recommend you to change the password for better security. If it was you please ignore the message or use forgot password service. Thank you.";
                                      MailMessage mM = new MailMessage();
                                      mM.From = new MailAddress("noreplyimagedit@gmail.com");
                                      mM.To.Add(name);
                                      mM.Body = body;
                                      mM.Subject = "Attention for account security from Imagedit.com";
                                      mM.IsBodyHtml = true;
                                      /*if (FileUpload1.HasFile)
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
                                      Response.Write("mail sent");

                                        SMS.APIType = SMSGateway.Site2SMS;
                                        SMS.MashapeKey = "uXK30jybwpzxzoQsA89nHI3jc78IHWzt";
                                        SMS.Username = "9819220080";
                                        SMS.Password = "13667";
                                        string message="Suspecious activity on your imagedIt account please see your email";
        
                                        //Single SMS
                                        try
                                        {
                                            SMS.SendSms(decrypt_mobile,message);
                                        }
                                        catch (Exception exp)
                                        { Response.Write(exp);};




                                      //Label3.Text = "Mail Send Successfully";
                                      //Label3.ForeColor = Color.Green;


                                  }
                                  catch (Exception ex)
                                  {
                                      Response.Write(ex);
                                  }
                              }

                          }
                          // con.Close();
                        
                      }
                      Response.Redirect("errorResult.aspx?id="+1);
                  }
              
              else
              {

                  Response.Redirect("errorResult.aspx?id=" + 1);
              }
          }

         
          con.Close();

    }
}