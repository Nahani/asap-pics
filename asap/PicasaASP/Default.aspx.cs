using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Runtime.InteropServices;

[ComVisible(true)]
public partial class _Default : System.Web.UI.Page
{
    int currentId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            currentId = (int)Session["id"];

        }
        catch (NullReferenceException nre)
        {
        }
        if (currentId != 0)
        {
            Response.Write("Already connected");
            Response.Redirect("Overview.aspx");
        }
    }
}
