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

        public bool Delete(string login)
        {
            return dataAccess.Delete_User(login);
        }

        User Get_User(string login)
        {
            return dataAccess.Get_User(login);
        }

        bool Check_password(string login, string pwd)
        {
            return dataAccess.Check_password(login, pwd);
        }

        public int Get_User_ID(String login)
        {
            return dataAccess.Get_Id_User(login);
        }
    }
}
