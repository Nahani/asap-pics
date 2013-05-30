using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PicasaASP.User_Service;

namespace PicasaASP
{
    public partial class Welcome : System.Web.UI.Page
    {
        public static UserServiceClient user_client = new UserServiceClient();
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

        protected void Login_Click(object sender, EventArgs e)
        {
            string login = UserBox.Text;
            string pwd = PassBox.Text;
            int id;
            if (!user_client.Check_password(login, pwd))
            {
                reponse.InnerText = "ERROR : Bad login/password combination.";
            }
            else
            {
                id = user_client.Get_User_ID(login);
                Session["id"] = id;
                Response.Redirect("Overview.aspx");
            }

        }
        protected void Subscription_Click(object sender, EventArgs e)
        {
            Response.Redirect("Subscription.aspx");
        }
    }
}