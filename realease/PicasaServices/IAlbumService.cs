/**
 * 	Fichier : IAlbumService.cs 
 * 
 * 	Version : 1.0.0 
 * 		- Association des méthodes de base de la BDD avec les services offerts
 * 
 * 	Auteurs : Théo BOURDIN, Alexandre BOURSIER & Nolan POTIER
 * 	
 * 	Résumé : Interface de services lien avec la base de données PICASA définissant les albums cibles. 
 * 
 */

using System;
using System.Collections.Generic;
using DB;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace PicasaServices
{
    [ServiceContract]
    public interface IAlbumService
    {
        /*
         * Ajouter un album cible dans la base de données
         * 
         * @param name  : Nom de l'album cible
         * @param idUser   : Identifiant de l'utilisateur propriétaire de l'album cible 
         * 
         * @return true si l'album a bien été ajouté, false le cas échéant
         * 
         */
        [OperationContract]
        bool Add(String name, int idProp);

       /*
        * Supprimer un album de la Base De Données
        * 
        * @param idProp  : l'identifiant du propriétaire cible
        * @param name  : le nom de l'album cible
        * 
        * @return true si l'album a bien été supprimé, false le cas échéant
        * 
        */
        [OperationContract]
        bool Delete(String name, int idProp);

        /*
         * Obtenir une liste d'albums appartenant à un utilisateur cible
         * 
         * @param idProp    : l'identifiant de l'utilisateur propriétaire des albums cibles
         * 
         * @return la liste d'albums s'ils ont bien été récupérés, null le cas échéant
         * 
         */
        [OperationContract]
        AlbumsResponse Get_Albums_From_User(int idProp);

        /*
         * Obtenir une liste d'albums appartenant à tout les autres utilisateurs cible
         * 
         * @param idProp    : l'identifiant de l'utilisateur propriétaire à exclure des albums cibles
         * 
         * @return la liste d'albums s'ils ont bien été récupérés, null le cas échéant
         * 
         */
        [OperationContract]
        AlbumsResponse Get_Albums_From_Other_Users(int idProp);
        
        /* 
         * Récupérer l'identifiant de l'l'album
         * 
         * @param name    : le nom de l'album cible
         * @param idProp  : l'identifiant de l'utilisateur cible
         * 
         * @return l'identifiant de l'album si il existe, -1 le cas échéant
         * 
         */
        [OperationContract]
        int Get_Album_ID(String name, int idProp);
    }

    [DataContract]
    public class AlbumsResponse
    {

        private List<Album> albums;

        [DataMember(Name = "Albums", Order = 0)]
        public List<Album> Albums
        {
            get { return albums; }
            set { albums = value; }
        }

    }
}
