using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Net.Mail;
using System.Drawing;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using ASPSnippets.SmsAPI;

public partial class register : System.Web.UI.Page
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

// get 1st random string 



    protected void Page_Load(object sender, EventArgs e)
    {
        //emailerror.Text="";
        //email.Text="";
    }



    private string Encrypt(string strToEncrypt, string strKey)
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
                byteBuff = System.Text.ASCIIEncoding.ASCII.GetBytes(strToEncrypt);
                return Convert.ToBase64String(objDESCrypto.CreateEncryptor().
                    TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            }
            catch (Exception ex)
            {
                return "Wrong Input. " + ex.Message;
            }
        }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
       SqlConnection con = new System.Data.SqlClient.SqlConnection(@"Server=tcp:es7b5g9w5e.database.windows.net,1433;Database=imagedit;User ID=archana@es7b5g9w5e;Password=systemerrorA1;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
        
        String encrypt_mobile="";
        String encrypt_name="";
        String us= email.Text;
        String name=username.Text;
        String pw=p1box.Text;
        String Question=secure.SelectedItem.Text;
        String Answer=ans.Text;
        String mobile=mobnumber.Text;
        con.Open(); 
        int result;
        string Rand1 = RandomString(20);
        string bodycode="<html><body>Authentication Code: "+Rand1+"<br/> Please visit the link given below to activate account on Imagedit.<br/> Enter the authentication code given above to complete the process.<br/><a  target=\"_blank\" href=\"http://imagedit.azurewebsites.net/validate.aspx\"> Click here to proceed</a></body></html>";


        //To encrypt the password


                 MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
                 UTF8Encoding encoder = new UTF8Encoding();
				Byte[] originalPassword;
             Byte[] hashedPassword;
             originalPassword = encoder.GetBytes(pw);
            hashedPassword = md5Hasher.ComputeHash(originalPassword);
         

           //create new instance of StringBuilder to save hashed data
            StringBuilder returnValue = new StringBuilder();

         //loop for each byte and add it to StringBuilder
         for (int i = 0; i < hashedPassword.Length; i++)
          {
               returnValue.Append(hashedPassword[i].ToString());
           }

         

                  string hashpwd =returnValue.ToString();
				  returnValue.Clear();
				  Byte[] originalemail;
				  Byte[] hashedemail;
				  originalemail = encoder.GetBytes(us);
				  hashedemail = md5Hasher.ComputeHash(originalemail);
         
         //loop for each byte and add it to StringBuilder
						 for (int i = 0; i < hashedemail.Length; i++)
						  {
							   returnValue.Append(hashedemail[i].ToString());
						   }

                  string hashemail =returnValue.ToString();
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



            try
            {
                encrypt_mobile=Encrypt(mobile,hashemail);
                encrypt_name=Encrypt(name,hashemail);
            }                      

            catch (Exception ex)
            {
                Response.Write("Wrong Input. " + ex.Message);
            }





           
        string Rand2 = RandomString(6);
            //checking database

        SqlCommand com = new SqlCommand("If NOT exists (SELECT * FROM client WHERE email =  @email) Insert into client(email,password,name,security,answer,captcha,mobile,m_captcha) values(@email, @password,@name, @security_question, @answer, @captcha,@mobile,@m_captcha)", con);
        {
            com.Parameters.AddWithValue("@email", hashemail);
            com.Parameters.AddWithValue("@password",hashpwd);
            com.Parameters.AddWithValue("@security_question",hashquestion);
            com.Parameters.AddWithValue("@answer",hashanswer);
            com.Parameters.AddWithValue("@captcha",Rand1);
            com.Parameters.AddWithValue("@m_captcha",Rand2);
            com.Parameters.AddWithValue("@mobile",encrypt_mobile);
            com.Parameters.AddWithValue("@name",encrypt_name);
            result=com.ExecuteNonQuery();
           Response.Write(result);
             Response.Write("hello");
                 Response.Write(us);
            }
         
        if (result>0)
        {   
            Response.Write("hello1");
            MailMessage mM = new MailMessage();
            mM.From = new MailAddress("noreplyimagedit@gmail.com");
            mM.To.Add(us);
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
            sC.Send(mM);
            Response.Write("hello");


            SMS.APIType = SMSGateway.Site2SMS;
            SMS.MashapeKey = "uXK30jybwpzxzoQsA89nHI3jc78IHWzt";
            SMS.Username = "9819220080";
            SMS.Password = "13667";
            string message="Authenticate code is: "+Rand2;
        
            //Single SMS
            try
            {
                SMS.SendSms(mobile, message);
            }
            catch (Exception exp)
            { Response.Write(exp);};
     

            con.Close();


             Response.Redirect("errorresult.aspx?id="+2+"&id1="+hashemail);

        }
        else
        {

      Response.Redirect("errorresult.aspx?id="+3);
        }

        con.Close();
       

    }
    protected void reset(object sender, EventArgs e)
    {
          email.Text="";
       username.Text="";
        p1box.Text="";
        secure.SelectedItem.Text="";
       ans.Text="";
       mobnumber.Text="";
    }


    
    }
    