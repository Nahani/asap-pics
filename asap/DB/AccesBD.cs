﻿/**
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
    public interface AccesBD
    {
        int Get_Id_Img(int idAlbum, String name);

        String Get_Name_Img(int idAlbum, int id);

        byte[] Get_Image(int id, int idAlbum);

        byte[] Get_Thumb(int id, int idAlbum);

        List<byte[]> Get_Image_From_Albums(int idAlbum);
        
        List<int> Get_Image_ID_From_Albums(int idAlbum);

        bool Add_Img(Img im);

        bool Delete_Img(int id, int idAlbum);

        Album Get_Album(String name, int idUser);

        List<Album> Get_Albums_From_User(int idUser);

        List<int> Get_AlbumsID_From_User(int idUser);

        List<Album> Get_Albums_From_Other_Users(int idUser);

        List<int> Get_AlbumsID_From_Other_Users(int idUser);

        String Get_Name_Album(int id);

        int Get_Id_Album(String name, int idProp);

        bool Exists_Album(int id);

        bool Add_Album(Album al);

        bool Delete_Album( int idProp, String name);

        int Get_Id_User(String login);

        bool Exists_User(String login);

        bool Add_User(User us);

        bool Delete_User(String login);

        User Get_User(String login = null, int id = 0);

        bool Check_password(String login, String password);

    }
}
