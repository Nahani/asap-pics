/**
 * 	Fichier : IImageService.cs 
 * 
 * 	Version : 1.0.0 
 * 		- Association des méthodes de base de la BDD avec les services offerts
 * 
 * 	Auteurs : Théo BOURDIN, Alexandre BOURSIER & Nolan POTIER
 * 	
 * 	Résumé : Interface de services lien avec la base de données PICASA définissant les images cibles. 
 * 
 */

using System;
using System.ServiceModel;
using System.IO;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace PicasaServices
{
    [ServiceContract]
    public interface IImageService
    {
        /* 
         * Ajouter une image cible dans la base de données
         * 
         * @param idAlbum   : le nom de l'album contenant
         * @param name      : le nom de l'image cible
         * @param img       : l'image cible sous forme d'un tableau de bytes
         * 
         * @return true si l'image a bien été ajoutée, false le cas échéant
         * 
         */
        [OperationContract]
        void Add(ImageUploadRequest data);

       /*
        * Supprimer une image de la Base De Données
        * 
        * @param id        : l'identifiant de l'image cible
        * @param idAlbum   : l'identifiant de l'album contenant
        * 
        * @return true si l'image a bien été supprimée, false le cas échéant
        * 
        */
        [OperationContract]
        bool Delete(int id, int idAlbum);


        /*
         * Récupérer une image
         * 
         * @param id        : l'identifiant de l'image cible
         * @param idAlbum   : le nom de l'album contenant
         * 
         * @return l'image si elle existe, null sinon
         * 
         */
        [OperationContract]
        ImageDownloadResponse Get_Image(ImageDownloadRequest data);

        /*
         * Récupérer le nom de l'image cible
         * 
         * @param id        : l'identifiant de l'image cible
         * @param idAlbum   : le nom de l'album contenant
         * 
         * @return le nom de l'image si elle existe, null sinon
         * 
         */
        [OperationContract]
        string Get_Image_Name(int id, int idAlbum);

        /* 
         * Récupérer l'identifiant de l'image cible
         * 
         * @param idAlbum   : le nom de l'album contenant
         * @param name      : le nom de l'image cible
         * 
         * @return l'id de l'image si elle existe, -1 sinon
         * 
         */
        [OperationContract]
        int Get_Image_ID(int idAlbum, string name);

        /* 
         * Récupérer toutes les images d'un album
         * 
         * @param idAlbum   : le nom de l'album contenant
         * 
         * @return la liste des images si elles existent, null sinon
         * 
         */
        [OperationContract]
        List<ImageDownloadResponse> Get_Images_From_Album(int idAlbum);

        /* 
         * Récupérer tous les identififiants d'images d'un album
         * 
         * @param idAlbum   : le nom de l'album contenant
         * 
         * @return la liste des images si elles existent, null sinon
         * 
         */
        [OperationContract]
        List<int> Get_Images_ID_From_Album(int idAlbum);
    }


    [MessageContract]
    public class ImageUploadRequest
    {
        [MessageHeader(MustUnderstand = true)]
        public ImageInfo ImageInfo;
        [MessageBodyMember(Order = 1)]
        public Stream ImageData;
    }

    [MessageContract]
    public class ImageDownloadResponse
    {
        [MessageBodyMember(Order = 1)]
        public Stream ImageData;
    }


    [MessageContract]
    public class ImageDownloadRequest
    {
        [MessageBodyMember(Order = 1)]
        public ImageInfo ImageInfo;
    }

    [DataContract]
    public class ImageInfo
    {
        [DataMember(Order = 1)]
        public int ID { get; set; }

        [DataMember(Order = 2, IsRequired = true)]
        public int Album { get; set; }

        [DataMember(Order = 3, IsRequired = true)]
        public string Name { get; set; }

    }
}