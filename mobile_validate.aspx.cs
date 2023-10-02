using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Data.SqlClient;

public partial class mobile_validate : System.Web.UI.Page
{
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





    protected void Page_Load(object sender, EventArgs e)
    {}
    protected void validateme(object sender, EventArgs e)
    {
        
          var id = Request.QueryString["id"];
        String encrypt_mobile="";
        string name=userid.Text;
        string random=random_number.Text;
        string captcha;



        try
            {
                encrypt_mobile=Encrypt(name,id);
            }                      

            catch (Exception ex)
            {
                Response.Write("Wrong Input. " + ex.Message);
            }



           
            SqlConnection con = new System.Data.SqlClient.SqlConnection(@"Server=tcp:es7b5g9w5e.database.windows.net,1433;Database=imagedit;User ID=archana@es7b5g9w5e;Password=systemerrorA1;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
            con.Open();
            
            



        SqlCommand cmd=new SqlCommand("Select m_captcha from client where mobile like @mobile ",con);
        {
              cmd.Parameters.AddWithValue("@mobile",encrypt_mobile);
              captcha = Convert.ToString(cmd.ExecuteScalar());
              if (!(string.IsNullOrEmpty(captcha)))
            {
              if(random==captcha)
              {     int count;
                  SqlCommand cmd1=new SqlCommand("update client set motp=1 where mobile like @mobile ",con);
                  {
                       cmd1.Parameters.AddWithValue("@mobile",encrypt_mobile);
                       count =cmd1.ExecuteNonQuery();

                  }
                 
                  if(count>0)
                  {

                
                      Response.Redirect("default.aspx");

                  }
                  else
                  {
                       validate_error.Text="There seems some problem.The data you enter is not correct";
                       userid.Text="";
                       random_number.Text="";
                      //Server.Transfer("mobile_validate.aspx>?id="+id);
                 
                  }
              }
              else 
              {
                  validate_error.Text="There seems some problem.The data you enter is not correct";
                  userid.Text="";
                  random_number.Text="";
               // Server.Transfer("mobile_validate.aspx>?id="+id);
                
                }

            }
            else 
              {
                   validate_error.Text="There seems some problem.The data you enter is not correct";
                   userid.Text="";
                   random_number.Text="";
                 //Server.Transfer("mobile_validate.aspx>?id="+id);
                 
              }
     }
     con.Close();






    

    }


    }