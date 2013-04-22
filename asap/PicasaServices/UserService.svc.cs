using System;
using DB;

namespace WcfService
{
    public class UserService : IUserService
    {

        private AccesBD_SQL dataAccess = AccesBD_SQL.Instance;

        public bool Add(String first_name, String last_name, String login, String mail, String pwd, bool level = false)
        {
            return dataAccess.Add_User(new User(first_name, last_name, login, pwd, mail, level));
        }

        public bool Delete(String login)
        {
            return dataAccess.Delete_User(login);
        }

        public User Get_User(String login)
        {
            return dataAccess.Get_User(login);
        }

        public bool Check_password(String login, String pwd)
        {
            return dataAccess.Check_password(login, pwd);
        }

        public int Get_User_ID(String login)
        {
            return dataAccess.Get_Id_User(login);
        }
    }
}
