/**
 * 	Fichier : ImageObject.cs 
 * 
 * 	Version : 1.0.0 
 * 		- Definition des actions disponible pour un objet Image
 * 
 * 	Auteurs : Théo BOURDIN, Alexandre BOURSIER & Nolan POTIER
 * 	
 * 	Résumé : Définition d'une Image et d'une collection d'images
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;

namespace PicasaWPF
{
    public class ImageObject
    {
        public String Name { get; set; }
        public byte[] Image { get; set; }
        public ImageObject(String Name, byte[] Image)
        {
            this.Name = Name;
            this.Image = Image;
        }

        public static byte[] GetBytes(Stream img)
        {
            MemoryStream m = new MemoryStream();
            img.CopyTo(m);
            return m.ToArray();
        }

        public static byte[] lireFichier(string chemin)
        {
            byte[] data = null;
            FileInfo fileInfo = new FileInfo(chemin);
            int nbBytes = (int)fileInfo.Length;
            FileStream fileStream = new FileStream(chemin, FileMode.Open,
            FileAccess.Read);
            BinaryReader br = new BinaryReader(fileStream);
            data = br.ReadBytes(nbBytes);
            br.Close();
            fileStream.Close();
            return data;
        }
    }

    public class ImageCollection : ObservableCollection<ImageObject>
    { }
}
