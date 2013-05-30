using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DB
{
    class Program
    {
        static AccesBD_SQL access = AccesBD_SQL.Instance;
        static void Main(string[] args)
        {
            /*
            User u = new User("a", "b", "c", "def", "abc", true);
            Console.WriteLine("bonjour");
            Console.WriteLine("bonjour");
            
             * 
            Album monAlb = new Album("test",access.Get_Id_User("c"));
            Console.WriteLine("bonjour");
            access.Add_Album(monAlb);
            Console.WriteLine("bonjour");
            */

            Img monImage = new Img(2, "No picture available", Image_Actions.getImageByte(@"C:\Users\user\Desktop\no_photo.jpg"));
            Console.WriteLine("bonjour");
            access.Add_Img(monImage);
            Console.WriteLine("bonjour");


            /*
            Img.Delete(1, 3);
            Img i = new Img(3, "1234", new Byte[18]);
            if(!i.Add())
                Console.WriteLine("l'image existe déjà");
            Console.WriteLine("Id de l'image : " + Img.Get_Id(3, "1234"));
            Console.WriteLine("Nom de l'image : " + Img.Get_Name(3, Img.Get_Id(3, "1234")));
            Console.WriteLine("Image : " + Img.Get_Image(Img.Get_Id(3, "1234"), 3).ToString());
            Console.ReadKey();*/
        }

    }
}