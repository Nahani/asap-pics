﻿/**
 * 	Fichier : AccessBD_SQL.cs 
 * 
 * 	Version : 1.0.0 
 * 		- Definition des échanges de base avec la base de données pour tout les types d'éntités.
 * 		- Récupération des valeurs d'attributs.
 * 
 * 	Auteurs : Théo BOURDIN, Alexandre BOURSIER & Nolan POTIER
 * 	
 * 	Résumé : Implémentation de l'interface AccessBD pour une base de données SQL. 
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;

namespace DB
{
    public class AccesBD_SQL : AccesBD
    {

        private static AccesBD_SQL instance;

        private AccesBD_SQL() { }

        public static AccesBD_SQL Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AccesBD_SQL();
                }
                return instance;
            }
        }

        /* 
         * Récupérer l'identifiant de l'image cible
         * 
         * @param idAlbum   : le nom de l'album contenant
         * @param name      : le nom de l'image cible
         * 
         * @return l'id de l'image si elle existe, -1 sinon
         * 
         */
        public int Get_Id_Img(int idAlbum, String name)
        {
            int idImage = -1;
            String req = "SELECT id FROM IMAGE WHERE name='" + name + "' AND idAlbum='" + idAlbum + "';";
            SqlDataReader reader = Connexion.execute_Select(req);
            while (reader.Read())
            {
                // Récupérer la colonne 0 (id) de la table formée par la requête précitée 
                idImage = reader.GetInt32(0);
            }
            Connexion.close();
            return idImage;
        }

        /* 
         * Récupérer le nom de l'image cible
         * 
         * @param id        : l'identifiant de l'image cible
         * @param idAlbum   : le nom de l'album contenant
         * 
         * @return le nom de l'image si elle existe, null sinon
         * 
         */
        public String Get_Name_Img(int idAlbum, int id)
        {
            String name = null;
            String req = "SELECT name FROM IMAGE WHERE id='" + id + "' AND idAlbum='" + idAlbum + "';";
            SqlDataReader reader = Connexion.execute_Select(req);
            while (reader.Read())
            {
                // Récupérer la colonne 0 (name) de la table formée par la requête précitée 
                name = reader.GetString(0);
            }
            Connexion.close();
            return name;
        }

        /*
	         * Obtenir une liste d'albums appartenant n'apparenant pas à l'utilisateur cible
         *
         * @param idUser    : l'identifiant de l'utilisateur propriétaire des albums cibles
	         *
         * @return la liste d'albums s'ils ont bien été récupérés, null le cas échéant
         *
         */
        public List<Album> Get_Albums_From_Other_Users(int idUser)
        {
            String req = "SELECT name,idUser FROM ALBUM WHERE idUser <> '" + idUser + "';";
            SqlDataReader reader = Connexion.execute_Select(req);
            List<Album> result = new List<Album>();
            while (reader.Read())
                result.Add(new Album(reader.GetString(0), reader.GetInt32(1)));
            Connexion.close();
            return result;
        }

        /* 
         * Récupérer une image
         * 
         * @param id        : l'identifiant de l'image cible
         * @param idAlbum   : le nom de l'album contenant
         * 
         * @return l'image si elle existe, null sinon
         * 
         */
        public byte[] Get_Image(int id, int idAlbum)
        {
            byte[] blob = null;
            String req = "SELECT size,image FROM IMAGE WHERE idAlbum = '" + idAlbum + "' AND id='" + id + "';";
            SqlDataReader reader = Connexion.execute_Select(req);

            if (reader.Read())
            {
                int size = reader.GetInt32(0);
                blob = new byte[size];
                reader.GetBytes(1, 0, blob, 0, size);
            }
            Connexion.close();
            return blob;
        }

        /* 
         * Récupérer toutes les images d'un album
         * 
         * @param idAlbum   : le nom de l'album contenant
         * 
         * @return les images si l'album n'esgt pas vide, null sinon
         * 
         */
        public List<byte[]> Get_Image_From_Albums(int idAlbum)
        {
            List<byte[]> blobs = new List<byte[]>();
            String req = "SELECT size,image FROM IMAGE WHERE idAlbum = '" + idAlbum + "';";
            SqlDataReader reader = Connexion.execute_Select(req);

            while (reader.Read())
            {
                int size = reader.GetInt32(0);
                byte[] blob = new byte[size];
                reader.GetBytes(1, 0, blob, 0, size);
                blobs.Add(blob);
            }
            Connexion.close();
            return blobs;
        }

        /* 
         * Récupérer tous les identifiants des images d'un album
         * 
         * @param idAlbum   : le nom de l'album contenant
         * 
         * @return les images si l'album n'esgt pas vide, null sinon
         * 
         */
        public List<int> Get_Image_ID_From_Albums(int idAlbum)
        {
            List<int> ids = new List<int>(); ;
            String req = "SELECT id FROM IMAGE WHERE idAlbum = '" + idAlbum + "';";
            SqlDataReader reader = Connexion.execute_Select(req);

            while (reader.Read())
            {
                ids.Add(reader.GetInt32(0));
            }
            Connexion.close();
            return ids;
        }

        /*
         * Ajouter une image dans la Base De Données
         * 
         * @return true si l'image a bien été ajoutée, false le cas échéant
         * 
         */
        public bool Add_Img(Img im)
        {
            bool flag = false;
            if (Get_Id_Img(im.IdAlbum, im.Name) == -1)
            {
                SqlCommand req = new SqlCommand(
               "INSERT INTO IMAGE (idAlbum, name,  size, image) " +
               "VALUES(@idAlbum, @name, @size, @image)", Connexion.Connection);
                req.Parameters.Add("@idAlbum", SqlDbType.Int).Value
                = im.IdAlbum;
                req.Parameters.Add("@name", SqlDbType.NChar, im.Name.Length).Value
               = im.Name;
                req.Parameters.Add("@size", SqlDbType.Int).Value = im.Image.Length;
                req.Parameters.Add("@image", SqlDbType.Image, im.Image.Length).Value
                = im.Image;

                flag = Connexion.execute_Request(req);
            }
            return flag;
        }

        /*
        * Supprimer une image de la Base De Données
        * 
        * @param id        : l'identifiant de l'image cible
        * @param idAlbum   : l'identifiant de l'album contenant
        * 
        * @return true si l'image a bien été supprimée, false le cas échéant
        * 
        */
        public bool Delete_Img(int id, int idAlbum)
        {
            bool flag = false;
            String req = "DELETE FROM IMAGE WHERE id = '" + id + "' AND idAlbum='" + idAlbum + "';";
            flag = Connexion.execute_Request(req);
            return flag;
        }

        /*
         * Obtenir un album selon son login ou son identifiant
         * 
         * @param name : le nom de l'album cible
         * @param idUser    : l'identifiant de l'utilisateur propriétaire de l'album cible
         * 
         * @return l'album s'il a bien été récupéré, null le cas échéant
         * 
         */
        public Album Get_Album(String name, int idUser)
        {
            String req = "SELECT nom, id FROM ALBUM WHERE name='" + name + "' AND idUser='" + idUser + "';";
            SqlDataReader reader = Connexion.execute_Select(req);
            Album a = null;
            if (reader.Read())
                a = new Album(reader.GetString(0), reader.GetInt32(1));
            Connexion.close();
            return a;
        }

        /*
         * Obtenir une liste d'albums appartenant à un utilisateur cible
         * 
         * @param idUser    : l'identifiant de l'utilisateur propriétaire des albums cibles
         * 
         * @return la liste d'albums s'ils ont bien été récupérés, null le cas échéant
         * 
         */
        public List<Album> Get_Albums_From_User(int idUser)
        {
            String req = "SELECT name FROM ALBUM WHERE idUser='" + idUser + "';";
            SqlDataReader reader = Connexion.execute_Select(req);
            List<Album> result = new List<Album>();
            while (reader.Read())
                result.Add(new Album(reader.GetString(0), idUser));
            Connexion.close();
            return result;
        }


        /* 
         * Récupérer l'identifiant de l'l'album
         * 
         * @param name    : le nom de l'album cible
         * @param idProp  : l'identifiant de l'utilisateur cible
         * 
         * @return l'identifiant de l'album si il existe, -1 le cas échéant
         * 
         */
        public int Get_Id_Album(String name, int idProp)
        {
            int idUser = -1;
            String req = "SELECT id FROM ALBUM WHERE name='" + name + "' AND idUser = '" + idProp + "';";
            SqlDataReader reader = Connexion.execute_Select(req);
            while (reader.Read())
            {
                // Récupérer la colonne 0 (id) de la table formée par la requête précitée 
                idUser = reader.GetInt32(0);
            }
            Connexion.close();
            return idUser;

        }

        /*
         * Vérifier si un album utilise déjà l'identifiant cible
         * 
         * @param id    : l'identifiant de l'album recherché 
         * 
         * @return true si l'album cible existe, false le cas échéant
         * 
         */
        public bool Exists_Album(int id)
        {
            String req = "SELECT * FROM ALBUM WHERE id='" + id + "'";
            SqlDataReader reader = Connexion.execute_Select(req);
            bool exists = false;
            if (reader.Read())
            {
                exists = true;
            }
            Connexion.close();
            return exists;
        }

        /*
         * Ajouter un album dans la Base De Données
         * 
         * @return true si l'album a bien été ajouté, false le cas échéant
         * 
         */
        public bool Add_Album(Album al)
        {
            /*
            bool flag = false;
            String req = "INSERT INTO ALBUM (name, date, idUser) VALUES ('" + al.Name + "','" + Convert.ToString(DateTime.Now) + "','" + al.IdUser + "');";
            flag = Connexion.execute_Request(req);
            return flag;
             */

            bool flag = false;
            if (Get_Id_Album(al.Name, al.IdUser) == -1)
            {
                SqlCommand req = new SqlCommand(
               "INSERT INTO ALBUM (name, date, idUser) " +
               "VALUES(@name, @date, @idUser)", Connexion.Connection);
                req.Parameters.Add("@name", SqlDbType.NChar, al.Name.Length).Value
                = al.Name;
                req.Parameters.Add("@date", SqlDbType.Date).Value
               = DateTime.Now;
                req.Parameters.Add("@idUser", SqlDbType.Int).Value = al.IdUser;


                flag = Connexion.execute_Request(req);
            }
            return flag;
        }


        /*
         * Supprimer un album de la Base De Données
         * 
         * @param idProp  : l'identifiant du propriétaire cible
         * @param name  : le nom de l'album cible
         * 
         * @return true si l'album a bien été supprimé, false le cas échéant
         * 
         */
        public bool Delete_Album(int idProp, String name)
        {
            bool flag = false;
            String req = "DELETE FROM ALBUM WHERE idUser = '" + idProp + "' AND name = '" + name + "';";
            flag = Connexion.execute_Request(req);
            return flag;
        }

        /*
         * Récupérer l'identifiant de l'utilisateur
         * 
         * @param login    : le login de l'utilisateur cible
         * 
         * @return l'identifiant de l'utlisateur si il existe, -1 le cas échéant
         * 
         */
        public int Get_Id_User(String login)
        {
            int idUser = -1;
            String req = "SELECT id FROM USERS WHERE login='" + login + "';";
            SqlDataReader reader = Connexion.execute_Select(req);
            while (reader.Read())
            {
                // Récupérer la colonne 0 (id) de la table formée par la requête précitée 
                idUser = reader.GetInt32(0);
            }
            Connexion.close();
            return idUser;

        }

        /*
         * Vérifier si un login est déjà existant
         * 
         * @param login    : le login de l'utilisateur cible
         * 
         * @return true si l'utilisateur cible existe, false le cas échéant
         * 
         */
        public bool Exists_User(String login)
        {
            String req = "SELECT * FROM USERS WHERE login='" + login + "'";
            SqlDataReader reader = Connexion.execute_Select(req);
            bool exists = false;
            if (reader.Read())
            {
                exists = true;
            }
            Connexion.close();
            return exists;
        }

        /*
         * Ajouter un utilisateur dans la Base De Données
         * 
         * @return true si l'utilisateur a bien été ajouté, false le cas échéant
         * 
         */
        public bool Add_User(User us)
        {
            bool flag = false;
            if (!Exists_User(us.Login))
            {
                String req = "INSERT INTO USERS (first_name, last_name, login, password, mail, lvl) VALUES ('" + us.First_name + "','" + us.Last_name + "','" + us.Login + "','" + MD5_Actions.GetMd5Hash(MD5.Create(), us.Password) + "','" + us.Mail + "','" + Convert.ToInt32(us.Level) + "');";
                flag = Connexion.execute_Request(req);
            }
            return flag;
        }

        /*
         * Supprimer un utilisateur de la Base De Données
         * 
         * @param login : le login de l'utilisateur cible
         * 
         * @return true si l'utilisateur a bien été supprimé, false le cas échéant
         * 
         */
        public bool Delete_User(String login)
        {
            bool flag = false;
            String req = "DELETE FROM USERS WHERE login = '" + login + "';";
            flag = Connexion.execute_Request(req);
            return flag;
        }

        /*
         * Obtenir un utilisateur selon son login ou son identifiant
         * 
         * @param login : le login de l'utilisateur cible
         * @param id    : l'identifiant de l'utilisateur cible
         * 
         * @return l'utilisateur s'il a bien été récupéré, null le cas échéant
         * 
         */
        public User Get_User(String login = null, int id = 0)
        {
            String req = null;
            User targeted_user = null;
            if (!id.Equals(0))
            {
                req = "SELECT * FROM USERS WHERE id='" + id + "'";
            }
            else if (!login.Equals(null))
            {
                req = "SELECT * FROM USERS WHERE login='" + login + "'";
            }
            SqlDataReader reader = Connexion.execute_Select(req);
            if (reader.Read())
            {
                targeted_user = new User(reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), true);
            }
            Connexion.close();
            return targeted_user;
        }

        /*
         * Vérifier la validité d'un mot de passe
         * 
         * @param login         : le login de l'utilisateur présumé
         * @param password      : le mot de passe de l'utilisateur présumé éponyme
         * 
         * @return true si le mot de passe est correct, false le cas échéant
         * 
         */
        public bool Check_password(String login, String password)
        {
            String req = "SELECT password FROM USERS WHERE login='" + login + "'";
            SqlDataReader reader = Connexion.execute_Select(req);
            bool flag = false;
            if (reader.Read())
            {
                if (MD5_Actions.VerifyMd5Hash(MD5.Create(), password, reader.GetString(0)))
                {
                    flag = true;
                }
            }
            Connexion.close();
            return flag;
        }

    }
}