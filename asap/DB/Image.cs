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

        // Identifiant de l'utilisateur propriété clé étrangère
        private int idUser;

        // Nom de l'image courante
        private string name;

        // Image cible
        private byte[] image;

        /** 
         * Constructeur normal d'une image
         * 
         * @param idUser    : le nom de l'utilisateur propriétaire
         * @param name      : le nom de l'image cible
         * @param img       : l'image cible sous forme d'un tableau de bytes
         * 
         */
        public Img(int idUser, string name, byte[] img)
        {
            this.id = 0;
            this.name = name;
            this.idUser = idUser;
            this.image = img;
        }

        /** 
         * Récupérer l'identifiant de l'image cible
         * 
         * @param idUser    : le nom de l'utilisateur propriétaire
         * @param name      : le nom de l'image cible
         * 
         * @return l'id de l'image si elle existe, -1 sinon
         * 
         */
        public static int Get_Id(int idUser, string name)
        {
            int idImage = -1;
            string req = "SELECT id FROM IMAGE WHERE name='" + name + "' AND idUser='" + idUser + "';";
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
         * @param idUser    : le nom de l'utilisateur propriétaire
         * 
         * @return le nom de l'image si elle existe, null sinon
         * 
         */
        public static string Get_Name(int idUser, int id)
        {
            string name = null;
            string req = "SELECT name FROM IMAGE WHERE id='" + id + "' AND idUser='" + idUser + "';";
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
         * @param idUser    : le nom de l'utilisateur propriétaire
         * 
         * @return l'image si elle existe, null sinon
         * 
         */
        public static byte[] Get_Image(int id, int idUser)
        {
            byte[] blob = null;
            String req = "SELECT size,image FROM IMAGE WHERE idUser = '" + idUser + "' AND id='" + id + "';";
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
            if (Get_Id(idUser, name) == -1)
            {
                String req = "INSERT INTO IMAGE (idUser, name, size, image) " + "VALUES('" + idUser + "', '" + name + "', '" + image.Length + "', '" + image + "')";
                flag = Connexion.execute_Request(req);
            }
            return flag;
        }

        /*
         * Supprimer une image de la Base De Données
         * 
         * @param id        : l'identifiant de l'image cible
         * @param idUser    : l'identifiant de l'utilisateur propriétaire
         * 
         * @return true si l'image a bien été supprimée, false le cas échéant
         * 
         */
        public static bool Delete(int id, int idUser)
        {
            bool flag = false;
            String req = "DELETE FROM IMAGE WHERE id = '" + id + "' AND idUser='" + idUser + "';";
            flag = Connexion.execute_Request(req);
            return flag;
        }

        public override string ToString()
        {
            return "{Image : \n n° = " + id + ", utilisateur = " + idUser + ", nom = " + name + ", taille = " + image.Length + "}";
        }

    }
}
