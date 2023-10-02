using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class share_View : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
        protected string Getlocation()
    {
        var location = Request.QueryString["location"];
        return location;
    }
}