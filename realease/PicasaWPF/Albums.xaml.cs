﻿/**
 * 	Fichier : Albums.xaml.cs 
 * 
 * 	Version : 1.0.0 
 * 		- Definition des actions disponible sur la fenêtre des albums (create, delete, view)
 * 
 * 	Auteurs : Théo BOURDIN, Alexandre BOURSIER & Nolan POTIER
 * 	
 * 	Résumé : Code behind de l'interface des albums
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PicasaWPF.Album_Service;
using PicasaWPF.Image_Service;
using System.IO;
using System.Drawing;

namespace PicasaWPF
{
    /// <summary>
    /// Logique d'interaction pour Album.xaml
    /// </summary>
    public partial class Albums : Window
    {

        private ImageCollection albumCollection;

        public Albums()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.ResizeMode = System.Windows.ResizeMode.NoResize;
            AlbumsResponse albums = MainWindow.album_client.Get_Albums_From_User(MainWindow.currentId);
            // On crée notre collection d'album et on y ajoute chaque album
            albumCollection = new ImageCollection();
            int albumId = 0;
            int[] img = null;
            Image_Service.ImageInfo infos = new Image_Service.ImageInfo();
            infos.ID = MainWindow.currentId;
            string img_name;
            byte[] image;

            foreach (Album a in albums.Albums)
            {
                albumId = MainWindow.album_client.Get_Album_ID(a.name, MainWindow.currentId);
               img = MainWindow.image_client.Get_Images_ID_From_Album(albumId); 
                   //  MessageBox.Show(Convert.ToString(img.Length));
                if (img.Length == 0)
                {
                    Bitmap test = Properties.Resources.no_photo;
                    MemoryStream ms = new MemoryStream();
                    test.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    image = ms.ToArray();
                }
                else
                {
                    Random r = new Random();
                    int idVignette = r.Next(img.Length);
                    img_name = MainWindow.image_client.Get_Image_Name(albumId, img[idVignette]);
                    infos.Name = img_name;
                    infos.Album = albumId;
                    infos.ID = img[idVignette];
                    image = ImageObject.GetBytes(MainWindow.image_client.Get_Image(infos));
                }
                albumCollection.Add(new ImageObject(a.name, image));
            }
           if (albums.Albums.Length == 0)
            {
                no_album.FontSize = 30;
                no_album.FontFamily = new System.Windows.Media.FontFamily("Aharoni");
                no_album.Foreground = System.Windows.Media.Brushes.Navy;
                no_album.Content = "NO ALBUM AVAILABLE IN THE DATABASE";
            }


            // On lie la collectionau ObjectDataProvider déclaré dans le fichier XAML
            ObjectDataProvider albumSource =
            (ObjectDataProvider)FindResource("AlbumCollection1");
            albumSource.ObjectInstance = albumCollection;
        }

        private void createAlbum_click(object sender, RoutedEventArgs e)
        {
            CreateAlbum createAlbum = new CreateAlbum();
            this.Close();
            createAlbum.Show();
        }

        private void printAlbum_click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            int idA = MainWindow.album_client.Get_Album_ID((string)b.Tag, MainWindow.currentId);
            DragAndDrop drag = new DragAndDrop(idA, (string)b.Tag);
            this.Close();
            drag.Show();
        }

        private void deleteAlbum_click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            string name = (string)b.Tag;
            int idAlbum = MainWindow.album_client.Get_Album_ID(name, MainWindow.currentId);
            int[] imgs = MainWindow.image_client.Get_Images_ID_From_Album(idAlbum);
            MessageBoxResult result = MessageBox.Show("Do you really want to remove album '" + name + "' ?", "Caption", MessageBoxButton.YesNo);
             if (result == MessageBoxResult.Yes)
             {
                 foreach (int i in imgs)
                 {
                     MainWindow.image_client.Delete(i, idAlbum);
                 }
                 MainWindow.album_client.Delete(name, MainWindow.currentId);
                 Albums a = new Albums();
                 this.Close();
                 a.Show();
             }
        }

        private void disconnect_click(object sender, RoutedEventArgs e)
        {
            MainWindow.currentId = 0;
            Login c = new Login();
            this.Close();
            c.Show();
        }
    }
}