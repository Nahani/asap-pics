using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DB;

namespace PicasaAdmin
{
    class Program
    {

        public static ServiceReference1.UserServiceClient user_client = new ServiceReference1.UserServiceClient();

        static void Main(string[] args)
        {
            connect();
            menu();
        }

        public static void menu()
        {
            Console.WriteLine("\nWelcome to the menu : \n\t 1 : Add a user\n\t 2 : Delete a user\n\t 3 : Add an album\n\t 4 : Delete an album\n\t 5 : Add an image\n\t 6 : Delete an image \n\t q : Exit");
            string targeted_action = Console.ReadLine();
            while (!check(targeted_action))
            {
                Console.WriteLine("Unkown command. Please start again.");
                targeted_action = Console.ReadLine();
            }
            switch (targeted_action)
            {
                case "1":
                    add_User();
                    break;
                case "2":
                    //delete_User();
                    break;
                case "3":
                    //add_Album();
                    break;
                case "4":
                    //delete_Album();
                    break;
                case "5":
                    //add_Image();
                    break;
                case "6":
                    //delete_Image();
                    break;
                case "q":
                    Console.WriteLine("Goodbye !");
                    Environment.Exit(1);
                    break;
                default:
                    Console.WriteLine("Unkown command. Please start again.");
                    menu();
                    break;
            }
        }


        public static bool check(string str)
        {
            bool is_ok;
            return (is_ok = ((str.Equals(null) || str.Length.Equals(0))
               ? false : true));
        }


        public static bool check_account(string login, string password)
        {
            User u = user_client.Get_User(login);
            if (user_client.Check_password(login, password))
            {
                if (u.Level)
                {
                    Console.WriteLine("Hello " + login + " ! \nWelcome to the admin workspace");
                }
                else
                {
                    Console.WriteLine("Hello " + login + " ! \nI'm sorry but this is an admin workspace.\n Goodbye !");
                    Environment.Exit(1);
                }
                return true;
            }
            Console.WriteLine("Invalid login/password combination");
            return false;
        }

        public static void connect()
        {
            Console.WriteLine("######## CONNECTION #########\n");
            Console.WriteLine("Login : ");
            string login = Console.ReadLine();
            while (!check(login))
            {
                Console.WriteLine("Invalid Login.");
                Console.WriteLine("Login : ");
                login = Console.ReadLine();
            }
            Console.WriteLine("Password : ");
            string password = Console.ReadLine();
            while (!check(login))
            {
                Console.WriteLine("Invalid password.");
                Console.WriteLine("Password : ");
                password = Console.ReadLine();
            }
            if (!check_account(login, password))
            {
                Console.WriteLine("Error occured during authentification. Please try again.");
                connect();
            }
        }

        public static void add_User()
        {
            Console.WriteLine("######## ADDING A USER #########\n");
            Console.WriteLine("First name : ");
            string first_name = Console.ReadLine();
            while (!check(first_name))
            {
                Console.WriteLine("First name is invalid.");
                Console.WriteLine("First name : ");
                first_name = Console.ReadLine();
            }
            Console.WriteLine("Last name: ");
            string last_name = Console.ReadLine();
            while (!check(last_name))
            {
                Console.WriteLine("Last name is invalid.");
                Console.WriteLine("Last name : ");
                last_name = Console.ReadLine();
            }
            Console.WriteLine("Login : ");
            string login = Console.ReadLine();
            while (!check(login))
            {
                Console.WriteLine("Login is invalid.");
                Console.WriteLine("Login : ");
                login = Console.ReadLine();
            }
            Console.WriteLine("Password : ");
            string pwd = Console.ReadLine();
            while (!check(pwd))
            {
                Console.WriteLine("Password is invalid.");
                Console.WriteLine("Password : ");
                pwd = Console.ReadLine();
            }
            Console.WriteLine("Mail : ");
            string mail = Console.ReadLine();
            while (!check(mail))
            {
                Console.WriteLine("Mail is invalid.");
                Console.WriteLine("Mail : ");
                mail = Console.ReadLine();
            }
            Console.WriteLine("Level (0:user/1:admin) : ");
            string level = Console.ReadLine();
            while (Convert.ToInt64(level) != 0 && Convert.ToInt64(level) != 1)
            {
                Console.WriteLine("Level is invalid.");
                Console.WriteLine("Level (0:user/1:admin) : ");
                level = Console.ReadLine();
            }

            if (user_client.Add(first_name, last_name, login, mail, pwd, Convert.ToBoolean(Convert.ToInt64(level))))
            {
                Console.WriteLine("Adding user with success !");
                Console.WriteLine(user_client.Get_User(login).ToString());
            }
            else
            {
                Console.WriteLine("Login already exists !");
            }
            menu();
        }
    }
}
