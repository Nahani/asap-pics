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
        private int id;
        private int idUser;
        private string name;
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
            id = 0;
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
         * @return true si l'image a bien été ajoutée, faux le cas échéant
         * 
         */
        public bool Add()
        {
            bool flag = false;
            if (Get_Id(idUser, name) == -1)
            {
                String req = "INSERT INTO Image (idUser, name, size, image) " + "VALUES('" + idUser + "', '" + name + "', '" + image.Length + "', '" + image + "')";
                flag = Connexion.execute_Request(req);
            }
            return flag;
        }

        /*
         * Supprimer une image de la Base De Données
         * 
         * @return true si l'image a bien été supprimée, faux le cas échéant
         * 
         */
        public static bool Delete(int id, int idUser)
        {
            bool flag = false;
            String req = "DELETE FROM Image WHERE id = '" + id + "' AND idUser='" + idUser + "';";
            flag = Connexion.execute_Request(req);
            return flag;
        }

        public override string ToString()
        {
            return "{Image : \n n° = " + id + ", utilisateur = " + idUser + ", nom = " + name + ", taille = " + image.Length + "}";
        }

    }
}
