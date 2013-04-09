/**
 * 	Fichier : Album.cs 
 * 
 * 	Version : 1.0.0 
 * 		- Definition des échanges de base avec la base de données : ADD & DELETE ;
 * 		- Récupération des valeurs d'attributs.
 * 
 * 	Auteurs : Théo BOURDIN, Alexandre BOURSIER & Nolan POTIER
 * 	
 * 	Résumé : Entité lien avec la base de données PICASA définissant un album contenant un ensemble d'images appartenant à un utilisateur. 
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace DB
{
    class Album
    {
        // Nom de l'album cible
        string name;

        // Utilisateur propriétaire de l'album cible
        int idUser;

        /*
         * Constructeur normal d'un album
         * 
         * @name  : Nom de l'album cible
         * @idUser   : Identifiant de l'utilisateur propriétaire de l'albumcible 
         * 
         */
        public Album(string name, int idUser)
        {
            this.name = name;
            this.idUser = idUser;
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
        public static Album Get_Album(String name, int idUser)
        {
            String req = "SELECT nom, id FROM ALBUM WHERE name='" + name + "' AND idUser='" + idUser + "';";
            SqlDataReader reader = Connexion.execute_Select(req);
            Album a = null;
            if (reader.Read())
                a = new Album(reader.GetString(0), reader.GetInt32(1));
            Connexion.close();
            return a;
        }

        public static List<Album> Get_Albums_From_User(int idUser)
        {
            String req = "SELECT nom, id FROM ALBUM WHERE idUser='" + idUser + "';";
            SqlDataReader reader = Connexion.execute_Select(req);
            List<Album> result = null;
            while (reader.Read())
                result.Add(new Album(reader.GetString(0), reader.GetInt32(1)));
            Connexion.close();
            return result;   
        }


        /*
         * Vérifier si un album utilise déjà l'identifiant cible
         * 
         * @param id    : l'identifiant de l'album recherché 
         * 
         * @return true si l'album cible existe, false le cas échéant
         * 
         */
        public static bool Exists(int id)
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
        public bool Add()
        {
            bool flag = false;
            String req = "INSERT INTO ALBUM (name, date, idUser) VALUES ('" + name + "','" + DateTime.Now + "','" + idUser + "');";
                flag = Connexion.execute_Request(req);    
            return flag;
        }

        /*
         * Supprimer un album de la Base De Données
         * 
         * @param  : l'identifiant de l'album cible
         * 
         * @return true si l'album a bien été supprimé, false le cas échéant
         * 
         */
        public static bool Delete(int id)
        {
            bool flag = false;
            String req = "DELETE FROM ALBUM WHERE id = '" + id + "';";
            flag = Connexion.execute_Request(req);
            return flag;
        }

        public override string ToString()
        {
            return "{Album = " + name + ", " + idUser + "}";
        }
    }
}
