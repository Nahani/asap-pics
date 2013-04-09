/**
 * 	Fichier : Image.cs 
 * 
 * 	Version : 1.0.0 
 * 		- Definition des échanges de base avec la base de données : ADD & DELETE ;
 * 		- Récupération des valeurs d'attributs.
 * 
 * 	Auteurs : Théo BOURDIN, Alexandre BOURSIER & Nolan POTIER
 * 	
 * 	Résumé : Entité lien avec la base de données PICASA définissant les images d'un album appartenant à un utilisateur cible. 
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

namespace DB
{
    public class Img
    {
        // Identifiant unique clé primaire de l'image cible
        private int id;

        // Identifiant de l'album contenant propriété clé étrangère
        private int idAlbum;

        // Nom de l'image courante
        private string name;

        // Image cible
        private byte[] image;

        /** 
         * Constructeur normal d'une image
         * 
         * @param idAlbum   : le nom de l'album contenant
         * @param name      : le nom de l'image cible
         * @param img       : l'image cible sous forme d'un tableau de bytes
         * 
         */
        public Img(int idAlbum, string name, byte[] img)
        {
            ;
            this.name = name;
            this.idAlbum = idAlbum;
            this.image = img;
        }

        /** 
         * Récupérer l'identifiant de l'image cible
         * 
         * @param idAlbum   : le nom de l'album contenant
         * @param name      : le nom de l'image cible
         * 
         * @return l'id de l'image si elle existe, -1 sinon
         * 
         */
        public static int Get_Id(int idAlbum, string name)
        {
            int idImage = -1;
            string req = "SELECT id FROM IMAGE WHERE name='" + name + "' AND idUser='" + idAlbum + "';";
            SqlDataReader reader = Connexion.execute_Select(req);
            while (reader.Read())
            {
                // Récupérer la colonne 0 (id) de la table formée par la requête précitée 
                idImage = reader.GetInt32(0);
            }
            Connexion.close();
            return idImage;
        }

        /** 
         * Récupérer le nom de l'image cible
         * 
         * @param id        : l'identifiant de l'image cible
         * @param idAlbum   : le nom de l'album contenant
         * 
         * @return le nom de l'image si elle existe, null sinon
         * 
         */
        public static string Get_Name(int idAlbum, int id)
        {
            string name = null;
            string req = "SELECT name FROM IMAGE WHERE id='" + id + "' AND idUser='" + idAlbum + "';";
            SqlDataReader reader = Connexion.execute_Select(req);
            while (reader.Read())
            {
                // Récupérer la colonne 0 (name) de la table formée par la requête précitée 
                name = reader.GetString(0);
            }
            Connexion.close();
            return name;
        }

        /** 
         * Récupérer une image
         * 
         * @param id        : l'identifiant de l'image cible
         * @param idAlbum   : le nom de l'album contenant
         * 
         * @return l'image si elle existe, null sinon
         * 
         */
        public static byte[] Get_Image(int id, int idAlbum)
        {
            byte[] blob = null;
            String req = "SELECT size,image FROM IMAGE WHERE idUser = '" + idAlbum + "' AND id='" + id + "';";
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
         * Ajouter une image dans la Base De Données
         * 
         * @return true si l'image a bien été ajoutée, false le cas échéant
         * 
         */
        public bool Add()
        {
            bool flag = false;
            if (Get_Id(idAlbum, name) == -1)
            {
                String req = "INSERT INTO IMAGE (idAlbum, name, size, image) " + "VALUES('" + idAlbum + "', '" + name + "', '" + image.Length + "', '" + image + "')";
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
        public static bool Delete(int id, int idAlbum)
        {
            bool flag = false;
            String req = "DELETE FROM IMAGE WHERE id = '" + id + "' AND idUser='" + idAlbum + "';";
            flag = Connexion.execute_Request(req);
            return flag;
        }

        public override string ToString()
        {
            return "{Image : \n n° = " + id + ", utilisateur = " + idAlbum + ", nom = " + name + ", taille = " + image.Length + "}";
        }

    }
}
