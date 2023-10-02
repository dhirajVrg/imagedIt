using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Hosting;
using System.Data.SqlClient;
public partial class save : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
           
    }
      [WebMethod(EnableSession = true)]

        public static void UploadImage(string imageData, string exts)
        {
            var email = HttpContext.Current.Session["email"];
            int id=(int)HttpContext.Current.Session["id"];
            string path="/Client/"+email+"/";
            string fileNameWitPath="";
            string folderPath = HttpContext.Current.Server.MapPath(path);
            if (exts == "jpg" || exts == "jpeg")
            {fileNameWitPath = folderPath + DateTime.Now.ToString().Replace("/", "-").Replace(" ", "- ").Replace(":", "") + ".jpeg"; }
            else if (exts == "png")
            {fileNameWitPath = folderPath + DateTime.Now.ToString().Replace("/", "-").Replace(" ", "- ").Replace(":", "") + ".png"; }
            else if (exts == "bmp")
            {fileNameWitPath = folderPath + DateTime.Now.ToString().Replace("/", "-").Replace(" ", "- ").Replace(":", "") + ".bmp"; }
            
            FileStream fs = new FileStream(fileNameWitPath, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);

            byte[] data = Convert.FromBase64String(imageData);

            bw.Write(data);
            bw.Close();





            SqlConnection con = new SqlConnection(@"Server=tcp:es7b5g9w5e.database.windows.net,1433;Database=imagedit;User ID=archana@es7b5g9w5e;Password=systemerrorA1;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
                con.Open();
            String location="/Client/"+email+"/"+fileNameWitPath;


            if (exts == "jpg" || exts == "jpeg")
                        {
                            location="/Client/"+email+"/"+DateTime.Now.ToString().Replace("/", "-").Replace(" ", "- ").Replace(":", "") + ".jpeg";
                            //fileNameWitPath = folderPath + DateTime.Now.ToString().Replace("/", "-").Replace(" ", "- ").Replace(":", "") + ".jpeg"; 
                }
            else if (exts == "png")
                        {
                            location="/Client/"+email+"/"+DateTime.Now.ToString().Replace("/", "-").Replace(" ", "- ").Replace(":", "") + ".png";
                            //fileNameWitPath = folderPath + DateTime.Now.ToString().Replace("/", "-").Replace(" ", "- ").Replace(":", "") + ".png";
                         }


            SqlCommand cmd=new SqlCommand("Update client set no_of_images=no_of_images+1 where id=@id ",con);
                 {
                  cmd.Parameters.AddWithValue("@id",id);
                  cmd.ExecuteScalar();
                  }

                  SqlCommand cmd1=new SqlCommand("insert into image(user_id,location) values(@id,@location) ",con);
                 {
                  cmd1.Parameters.AddWithValue("@id",id);
                  cmd1.Parameters.AddWithValue("@location",location);
                  cmd1.ExecuteScalar();
                  }


                  con.Close();

            //return "imageData";

        }



}