using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PicasaAdmin.UserService;
using PicasaAdmin.AlbumService;
using PicasaAdmin.ImageService;

namespace PicasaAdmin
{
    class Program
    {


        public static UserServiceClient user_client = new UserServiceClient();
        public static AlbumServiceClient album_client = new AlbumServiceClient();
        public static ImageServiceClient image_client = new ImageServiceClient();

        static void Main(string[] args)
        {
            connect();
            menu();
        }

        public static void menu()
        {
            Console.WriteLine("\nWelcome to the menu : \n\t 1 : Add a user\n\t 2 : Delete a user\n\t 3 : Add an album\n\t 4 : Delete an album\n\t 5 : Delete an image \n\t q : Exit");
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
                    delete_User();
                    break;
                case "3":
                    add_Album();
                    break;
                case "4":
                    delete_Album();
                    break;
                case "5":
                    delete_Image();
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
            if (user_client.Check_password(login, password))
            {
                /* if (user_client.Get_User_Level(login).Equals(1))
                 {*/
                Console.WriteLine("Hello " + login + " ! \nWelcome to the admin workspace");
                /*}
                else
                {
                    Console.WriteLine("Hello " + login + " ! \nI'm sorry but this is an admin workspace.\n Goodbye !");
                    Environment.Exit(1);
                }*/
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
                Console.WriteLine("Login you have filled is empty.");
                Console.WriteLine("Login : ");
                login = Console.ReadLine();
            }
            if (user_client.Get_User_ID(login).Equals(-1))
            {
                Console.WriteLine("The login you have filled doesn't exists in the DataBase\n");
                connect();
            }
            else
            {
                Console.WriteLine("Password : ");
                string password = Console.ReadLine();
                while (!check(password))
                {
                    Console.WriteLine("Password you have filled is empty.");
                    Console.WriteLine("Password : ");
                    password = Console.ReadLine();
                }
                if (!check_account(login, password))
                {
                    Console.WriteLine("Error occured during authentification. Please try again.");
                    connect();
                }
            }

        }

        public static void add_User()
        {
            Console.WriteLine("######## ADDING AN USER #########\n");
            Console.WriteLine("First name : ");
            string first_name = Console.ReadLine();
            while (!check(first_name))
            {
                Console.WriteLine("First name you have filled is empty.");
                Console.WriteLine("First name : ");
                first_name = Console.ReadLine();
            }
            Console.WriteLine("Last name: ");
            string last_name = Console.ReadLine();
            while (!check(last_name))
            {
                Console.WriteLine("Last name you have filled is empty.");
                Console.WriteLine("Last name : ");
                last_name = Console.ReadLine();
            }
            Console.WriteLine("Login : ");
            string login = Console.ReadLine();
            while (!check(login))
            {
                Console.WriteLine("Login you have filled is empty.");
                Console.WriteLine("Login : ");
                login = Console.ReadLine();
            }
            if (user_client.Get_User_ID(login).Equals(-1))
            {
                Console.WriteLine("Password : ");
                string pwd = Console.ReadLine();
                while (!check(pwd))
                {
                    Console.WriteLine("Password you have filled is empty.");
                    Console.WriteLine("Password : ");
                    pwd = Console.ReadLine();
                }
                Console.WriteLine("Mail : ");
                string mail = Console.ReadLine();
                while (!check(mail))
                {
                    Console.WriteLine("Mail you have filled is empty.");
                    Console.WriteLine("Mail : ");
                    mail = Console.ReadLine();
                }
                Console.WriteLine("Level (0:user/1:admin) : ");
                string level = Console.ReadLine();
                while (Convert.ToInt64(level) != 0 && Convert.ToInt64(level) != 1)
                {
                    Console.WriteLine("Level you have filled is different from 0 or 1.");
                    Console.WriteLine("Level (0:user/1:admin) : ");
                    level = Console.ReadLine();
                }

                if (user_client.Add(first_name, last_name, login, mail, pwd, Convert.ToBoolean(Convert.ToInt64(level))))
                {
                    Console.WriteLine("Adding user with success !");
                    //Console.WriteLine(user_client.Get_User(login).ToString());
                }
                else
                {
                    Console.WriteLine("Login already exists !");
                }
                menu();
            }
            else
            {
                Console.WriteLine("The login you have filled already exists in the DataBase.");
                menu();
            }

        }

        public static void delete_User()
        {
            Console.WriteLine("######## DELETING AN USER #########\n");
            Console.WriteLine("Login : ");
            String login = Console.ReadLine();
            while (!check(login))
            {
                Console.WriteLine("Login you have filled is empty.");
                Console.WriteLine("Login : ");
                login = Console.ReadLine();
            }
            if (user_client.Get_User_ID(login).Equals(-1))
            {
                Console.WriteLine("The login you have filled doesn't exists in the DataBase\n");
                menu();
            }
            else
            {
                if (user_client.Delete(login))
                {
                    Console.WriteLine("User deleted with success !");
                }
                else
                {
                    Console.WriteLine("User doesn't have been deleted");
                }
                menu();
            }
        }

        public static void add_Album()
        {
            Console.WriteLine("######## ADDING AN ALBUM #########\n");
            Console.WriteLine("Album name : ");
            String name = Console.ReadLine();
            while (!check(name))
            {
                Console.WriteLine("Name you have filled is empty.");
                Console.WriteLine("Album name : ");
                name = Console.ReadLine();
            }
            Console.WriteLine("Login of album's owner : ");
            String prop = Console.ReadLine();
            while (!check(prop))
            {
                Console.WriteLine("Login you have filled is empty.");
                Console.WriteLine("Login of album's owner : ");
                prop = Console.ReadLine();
            }
            if (user_client.Get_User_ID(prop).Equals(-1))
            {
                Console.WriteLine("The login of the abum's owner you have filled doesn't exists in the DataBase.");
                menu();
            }
            else
            {
                if (album_client.Add(name, user_client.Get_User_ID(prop)))
                {
                    Console.WriteLine("Album added with sucess !");
                }
                else
                {
                    Console.WriteLine("A problem occurs.");
                }
                menu();
            }
        }

        public static void delete_Album()
        {
            Console.WriteLine("######## DELETING AN ALBUM #########\n");
            Console.WriteLine("Login of album's owner :");
            String prop = Console.ReadLine();
            while (!check(prop))
            {
                Console.WriteLine("Login you have filled is empty.");
                Console.WriteLine("Login of album's owner :");
                prop = Console.ReadLine();
            }
            if (user_client.Get_User_ID(prop).Equals(-1))
            {
                Console.WriteLine("The login of the album's owner you have filled doesn't exists in the DataBase.");
                menu();
            }
            else
            {
                Console.WriteLine("Name of album : ");
                String name = Console.ReadLine();
                while (!check(name))
                {
                    Console.WriteLine("Name you have filled is empty.");
                    Console.WriteLine("Name of album : ");
                    name = Console.ReadLine();
                }
                if (album_client.Get_Album_ID(name, user_client.Get_User_ID(prop)).Equals(-1))
                {
                    Console.WriteLine("The owner you have filled doesn't have any album called " + name + ".");
                    menu();
                }
                else
                {
                    if (album_client.Delete(name, user_client.Get_User_ID(prop)))
                    {
                        Console.WriteLine("Album deleted with success !");
                    }
                    else
                    {
                        Console.WriteLine("Album doesn't have been deleted.");
                    }
                    menu();
                }
            }
        }

        public static void delete_Image()
        {
            Console.WriteLine("######## ADDING AN ALBUM #########\n");
            Console.WriteLine("Name of the picture :");
            String name = Console.ReadLine();
            while (!check(name))
            {
                Console.WriteLine("Name of the picture you have filled is empty.");
                Console.WriteLine("Name of the picture :");
                name = Console.ReadLine();
            }
            Console.WriteLine("Name of album : ");
            String name_album = Console.ReadLine();
            while (!check(name_album))
            {
                Console.WriteLine("Name of album you have filled is empty.");
                Console.WriteLine("Name of album : ");
                name_album = Console.ReadLine();
            }
            Console.WriteLine("Login of album's owner :");
            String prop = Console.ReadLine();
            while (!check(prop))
            {
                Console.WriteLine("Login you have filled is empty.");
                Console.WriteLine("Login of album's owner :");
                prop = Console.ReadLine();
            }
            if (user_client.Get_User_ID(prop).Equals(-1))
            {
                Console.WriteLine("The login of album's owner you have filled doesn't exists in the DataBase.");
                menu();
            }
            else
            {
                if (album_client.Get_Album_ID(name_album, user_client.Get_User_ID(prop)).Equals(-1))
                {
                    Console.WriteLine("The owner you have filled doesn't have any album called " + name_album + ".");
                    menu();
                }
                else
                {
                    if (image_client.Get_Image_ID(album_client.Get_Album_ID(name_album, user_client.Get_User_ID(prop)), name).Equals(-1))
                    {
                        Console.WriteLine("The album " + name_album + " doesn't have any picture called " + name + ".");
                        menu();
                    }
                    else
                    {
                        if (image_client.Delete(image_client.Get_Image_ID(album_client.Get_Album_ID(name_album, user_client.Get_User_ID(prop)), name), album_client.Get_Album_ID(name_album, user_client.Get_User_ID(prop))))
                        {
                            Console.WriteLine("Picture successfully deleted !");
                        }
                        else
                        {
                            Console.WriteLine(" Picture doesn't have been deleted");
                        }
                        menu();
                    }
                }
            }
        }
    }
}
