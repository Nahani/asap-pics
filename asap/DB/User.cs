using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace DB
{
    class User
    {

        private int idUser;
        private String first_name;
        private String last_name;
        private String login;
        private String password;
        private String mail;
        private bool level;

        /*
         * Constructeur normal d'un utilisateur
         * 
         * @first_name  : Nom de l'utilisateur cible
         * @last_name   : Prénom de l'utilisateur cible 
         * @login       : Login de l'utilisateur cible
         * @password    : Mot de Passe de l'utilisateur cible
         * @mail        : Adresse email de l'utilisateur cible
         * @level       : Niveau d'autoritsation de l'utilisateur cible :
         *                  - false : utilisateur classique 
         *                  - true  : administrateur
         * 
         */
        public User(String first_name, String last_name, String login, String password, String mail, bool level = false)
        {
            this.idUser = 0;
            this.first_name = first_name;
            this.last_name = last_name;
            this.login = login;
            this.password = password;
            this.mail = mail;
            this.level = level;
        }

        /** 
         * Récupérer l'identifiant de l'utilisateur
         * 
         * @param login    : le login de l'utilisateur cible
         * 
         * @return l'identifiant de l'utlisateur si il existe, -1 le cas échéant
         * 
         */
        public static int Get_Id(string login)
        {
            int idUser = -1;
            string req = "SELECT id FROM USERS WHERE login='" + login + "';";
            SqlDataReader reader = Connexion.execute_Select(req);
            while (reader.Read())
            {
                // Récupérer la colonne 0 (id) de la table formée par la requête précitée 
                idUser = reader.GetInt32(0);
            }
            Connexion.close();
            return idUser;
           
        }

        /** 
         * Vérifier si un login est déjà existant
         * 
         * @param login    : le login de l'utilisateur cible
         * 
         * @return true si l'utilisateur cible existe, false le cas échéant
         * 
         */
        public static bool Exists(string login)
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
        public bool Add()
        {
            bool flag = false;
            if (!Exists(login))
            {
                String req = "INSERT INTO USERS (first_name, last_name, login, password, mail, lvl) VALUES ('" + first_name + "','" + last_name + "','" + login + "','" + MD5_Actions.GetMd5Hash(MD5.Create(), password) + "','" + mail + "','" + Convert.ToInt32(level) + "');";
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
        public static bool Delete(string login)
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
         * @return true si l'utilisateur a bien été supprimé, false le cas échéant
         * 
         */
        public static User Get_User(String login = "", int id = 0)
        {
            String req = null;
            User targeted_user = null;
            if (!id.Equals(0))
            {
                req = "SELECT * FROM USERS WHERE id='" + id + "'";
            }
            if (!login.Equals(null))
            {
                req = "SELECT * FROM USERS WHERE login='" + login + "'";
            }
            SqlDataReader reader = Connexion.execute_Select(req);
            if (reader.Read())
            {
                targeted_user = new User(reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), Convert.ToBoolean(reader.GetInt32(6)));
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
        public static bool Check_password(string login, string password)
        {
            String req = "SELECT password FROM USERS WHERE login='" + login + "'";
            SqlDataReader reader = Connexion.execute_Select(req);
            bool flag = false;
            if (reader.Read()){
                if (MD5_Actions.VerifyMd5Hash(MD5.Create(), password, reader.GetString(0)))
                {
                    flag = true;
                }
                }
            Connexion.close();
            return flag;
        }

        public override string ToString()
        {
            return "{Utilisateur = " + first_name + ", " + last_name + ", " + login + ", " + password + ", " + mail + ", " + level + "}";
        }
    }
}
