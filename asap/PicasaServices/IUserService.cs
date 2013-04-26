/**
 * 	Fichier : IUserService.cs 
 * 
 * 	Version : 1.0.0 
 * 		- Association des méthodes de base de la BDD avec les services offerts
 * 
 * 	Auteurs : Théo BOURDIN, Alexandre BOURSIER & Nolan POTIER
 * 	
 * 	Résumé : Interface de services lien avec la base de données PICASA définissant les utilisateurs cibles. 
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DB;

namespace WcfService
{
    [ServiceContract]
    public interface IUserService
    {
        /*
         * Ajouter un utilisateur cible normal d'un utilisateur
         * 
         * @param first_name  : Nom de l'utilisateur cible
         * @param last_name   : Prénom de l'utilisateur cible 
         * @param login       : Login de l'utilisateur cible
         * @param password    : Mot de Passe de l'utilisateur cible
         * @param mail        : Adresse email de l'utilisateur cible
         * @param level       : Niveau d'autoritsation de l'utilisateur cible :
         *                  - false : utilisateur classique 
         *                  - true  : administrateur
         * 
         * @return true si l'utilisateur a bien été ajouté, false le cas échéant
         */
        [OperationContract]
        bool Add(String first_name, String last_name, String login, String mail, String pwd, bool level);

        /*
         * Supprimer un utilisateur de la Base De Données
         * 
         * @param login : le login de l'utilisateur cible
         * 
         * @return true si l'utilisateur a bien été supprimé, false le cas échéant
         * 
         */
        [OperationContract]
        bool Delete(String login);

       /*
        * Vérifier la validité d'un mot de passe
        * 
        * @param login         : le login de l'utilisateur présumé
        * @param password      : le mot de passe de l'utilisateur présumé éponyme
        * 
        * @return true si le mot de passe est correct, false le cas échéant
        * 
        */
        [OperationContract]
        bool Check_password(String login, String pwd);

        /* 
         * Récupérer l'identifiant de l'utilisateur
         * 
         * @param login    : le login de l'utilisateur cible
         * 
         * @return l'identifiant de l'utlisateur si il existe, -1 le cas échéant
         * 
         */
        [OperationContract]
        int Get_User_ID(String login);

        /* 
         * Récupérer le niveau d'autorisation de l'utilisateur
         * 
         * @param login    : le login de l'utilisateur cible
         * 
         * @return true si l'utilisateur est un administrateur, false le cas échéant
         * 
         */
        [OperationContract]
        bool Get_User_Level(String login);

        [OperationContract]
        User Get_User(String login);
    }
}
