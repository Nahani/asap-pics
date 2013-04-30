using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PicasaASP.User_Service;

namespace PicasaASP
{
    public partial class Subscription : System.Web.UI.Page
    {
        public static UserServiceClient user_client = new UserServiceClient();

        protected void Subscription_Click(object sender, EventArgs e)
        {
            string first_name = FN.Text;
            string last_name = LN.Text;
            string login = LO.Text;
            string pwd = PW.Text;
            string mail = MA.Text;
            int id;
            if (!user_client.Add(first_name, last_name, login, mail, pwd, false))
            {
                reponse.InnerText = "ERROR : Subscription hasn't turned to succeed.";
            }
            else
            {
                id = user_client.Get_User_ID(login);
                Session["id"] = id;
                Response.Redirect("Overview.aspx");
            }

        }
    }
}