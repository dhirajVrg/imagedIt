using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class deleteImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
            var id = Request.QueryString["id"];
            int u_id=(int)Session["id"];
            //String location;
           
           SqlConnection con = new System.Data.SqlClient.SqlConnection(@"Server=tcp:es7b5g9w5e.database.windows.net,1433;Database=imagedit;User ID=archana@es7b5g9w5e;Password=systemerrorA1;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
             con.Open();
             
             SqlCommand cmd= new SqlCommand("delete from image where image_id=@image_id",con);
             {
             cmd.Parameters.AddWithValue("@image_id",id);
             cmd.ExecuteNonQuery();
             }

              SqlCommand cmd1=new SqlCommand("Update client set no_of_images=no_of_images-1 where id=@id ",con);
                 {
                  cmd1.Parameters.AddWithValue("@id",u_id);
                  cmd1.ExecuteScalar();
                  }
            con.Close();
            Response.Redirect("delete.aspx");

    }
}