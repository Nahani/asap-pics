/**
 * 	Fichier : AccessBD.cs 
 * 
 * 
 * 	Auteurs : Théo BOURDIN, Alexandre BOURSIER & Nolan POTIER
 * 	
 * 	Résumé : Interface regroupant toutes les méthodes d'accés à la base de données.
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;




namespace DB
{
    interface AccesBD
    {
        int Get_Id_Img(int idAlbum, string name);

        string Get_Name_Img(int idAlbum, int id);

        byte[] Get_Image(int id, int idAlbum);

        bool Add_Img(Img im);

        bool Delete_Img(int id, int idAlbum);

        Album Get_Album(String name, int idUser);

        List<Album> Get_Albums_From_User(int idUser);

        bool Exists_Album(int id);

        bool Add_Album(Album al);

        bool Delete_Album(int id);

        int Get_Id_User(string login);

        bool Exists_User(string login);

        bool Add_User(User us);

        bool Delete_User(string login);

        User Get_User(String login, int id);

        bool Check_password(string login, string password);

    }
}
