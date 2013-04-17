using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DB
{
    class Program
    {
        static void Main(string[] args)
        {
            User u = new User("a", "b", "c", "def", "abc", true);
            AccesBD_SQL access = new AccesBD_SQL();
            access.Add_User(u);
            Console.WriteLine(access.Check_password("c","def"));
            Console.WriteLine(access.Check_password("c", "defg"));

            /*Img.Delete(1, 3);
            Img i = new Img(3, "1234", new Byte[18]);
            if(!i.Add())
                Console.WriteLine("l'image existe déjà");
            Console.WriteLine("Id de l'image : " + Img.Get_Id(3, "1234"));
            Console.WriteLine("Nom de l'image : " + Img.Get_Name(3, Img.Get_Id(3, "1234")));
            Console.WriteLine("Image : " + Img.Get_Image(Img.Get_Id(3, "1234"), 3).ToString());*/
            Console.ReadKey();
        }
    }
}
