using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class editor : System.Web.UI.Page
{
   protected void Page_Load(object sender, EventArgs e)
    {

    }
     protected string Getlocation()
        {

            var id = Request.QueryString["id"];
            String location;
            SqlConnection con = new SqlConnection(@"Server=tcp:es7b5g9w5e.database.windows.net,1433;Database=imagedit;User ID=archana@es7b5g9w5e;Password=systemerrorA1;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
             con.Open();
             
             SqlCommand cmd= new SqlCommand("select location from image where image_id=@image_id",con);
             {
             cmd.Parameters.AddWithValue("@image_id",id);
             location=Convert.ToString(cmd.ExecuteScalar());
             }
            return location; // something like this
        }



}