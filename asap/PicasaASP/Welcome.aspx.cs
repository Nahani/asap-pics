using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PicasaASP
{
    public partial class Welcome : System.Web.UI.Page
    {

        public static PicasaASP.ServiceReference1.UserServiceClient user_client = new PicasaASP.ServiceReference1.UserServiceClient();

        protected void Login_Click(object sender, EventArgs e)
        {
            string login = UserBox.Text;
            string pwd = PassBox.Text;
            int id;
            if (!user_client.Check_password(login, pwd))
            {
                Response.Write("ERROR : Bad login/password combination.");
            }
            else
            {
                id = user_client.Get_User_ID(login);
                Session["id"] = id;
                Response.Redirect("Albums.aspx");
            }

        }

        protected void Subscription_Click(object sender, EventArgs e)
        {
            Response.Redirect("Subscription.aspx");
        }
    }
}