﻿/**
 * 	Fichier : DragAndDrop.xaml.cs 
 * 
 * 	Version : 1.0.0 
 * 		- Definition des actions disponible sur la fenêtre de visualisation d'un album (DragAndDrop, delete, local directory)
 * 
 * 	Auteurs : Théo BOURDIN, Alexandre BOURSIER & Nolan POTIER
 * 	
 * 	Résumé : Code behind de l'interface de visualisation d'un album
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
using System.IO;
using System.Collections;
using System.Windows.Controls.Primitives;
using System.Threading;

namespace PicasaWPF
{
    /// <summary>
    /// Logique d'interaction pour DragAndDrop.xaml
    /// </summary>
    public partial class DragAndDrop : Window
    {
        private ImageCollection imageCollection1;
        private ImageCollection imageCollection2;
        private static string PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        private int idAlbum;
        private ListBox dragSource = null;


        public DragAndDrop(int IdAlbum, String nameAlbum)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.ResizeMode = System.Windows.ResizeMode.NoResize;
            this.idAlbum = IdAlbum;
            this.Title = "Images From " + nameAlbum;
            // On crée notre collection d'image et on y ajoute deux images
            imageCollection1 = new ImageCollection();
            Dictionary<string, byte[]> files = Read_Images_From_Local_Folder(PATH);
            for (int i = 0; i < files.Values.Count; i++)
            {
                imageCollection1.Add(new ImageObject(files.Keys.ElementAt(i), files.Values.ElementAt(i)));
            }
            // On lie la collection au ObjectDataProvider déclaré dans le fichier XAML
            ObjectDataProvider imageSource =
            (ObjectDataProvider)FindResource("ImageCollection1");
            imageSource.ObjectInstance = imageCollection1;

            no_img_db.Visibility = Visibility.Visible;
            no_img_db.FontSize = 30;
            no_img_db.Foreground = Brushes.Crimson;
            no_img_db.FontFamily = new FontFamily("Aharoni");
            no_img_db.Content = "LOADING...";

            Thread th = new Thread(new ParameterizedThreadStart(Read_Images_From_DB));
            th.Start(idAlbum);



           
        }

        private static void writeInFile(string chemin, byte[] file)
        {
            try
            {
                FileStream fs = new FileStream(chemin, FileMode.Create);
                foreach (byte b in file)
                {
                    fs.WriteByte(b);
                }
                fs.Close();
            }
            catch (Exception ex)
            {
                MessageBoxResult result = MessageBox.Show(ex.Message);
            }
        }

        public void ChooseFolder(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                PATH = folderBrowserDialog1.SelectedPath;
                local_maj();
            }
        }

        private Dictionary<string, byte[]> Read_Images_From_Local_Folder(string path)
        {
            Dictionary<string, byte[]> files = new Dictionary<string, byte[]>();
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            IEnumerable<FileInfo> filesInfo = dirInfo.EnumerateFiles();
            foreach (FileInfo fi in filesInfo.ToList())
            {
                string tmp = fi.FullName;
                string[] tabTmp = tmp.Split('.');
                if (tabTmp[tabTmp.Length - 1].ToLower() == "jpg" || tabTmp[tabTmp.Length - 1].ToLower() == "png" || tabTmp[tabTmp.Length - 1].ToLower() == "jpeg" || tabTmp[tabTmp.Length - 1].ToLower() == "bmp")
                {
                    files.Add(fi.Name.Split('.')[0], ImageObject.lireFichier(fi.FullName));
                }
            }
            if (files.Count == 0)
            {
                no_img.Visibility = Visibility.Visible;
                no_img.FontSize = 30;
                no_img.Foreground = Brushes.Crimson;
                no_img.FontFamily = new FontFamily("Aharoni");
                no_img.Content = "NO PICTURES AVAILABLE IN THE LOCAL FOLDER";
            }
            else if (no_img.Visibility == Visibility.Visible)
                no_img.Visibility = Visibility.Collapsed;
            return files;
        }

        private void Read_Images_From_DB(Object idAlbum)
        {
            imageCollection2 = new ImageCollection();

            
            Dictionary<string, byte[]> files = new Dictionary<string, byte[]>();
            int[] idImages = MainWindow.image_client.Get_Images_ID_From_Album((int)idAlbum);
            foreach (int id in idImages)
            {
                Image_Service.ImageInfo img = new Image_Service.ImageInfo();
                img.ID = id;
                img.Album = (int)idAlbum;
                img.Name = MainWindow.image_client.Get_Image_Name(img.Album, id);
                byte[] bytes = ImageObject.GetBytes(MainWindow.image_client.Get_Image(img));
                imageCollection2.Add(new ImageObject(img.Name, bytes));
            }

            this.Dispatcher.Invoke(new Action(check));
            // On lie la collectionau ObjectDataProvider déclaré dans le fichier XAML
            ObjectDataProvider imageSrc = (ObjectDataProvider)FindResource("ImageCollection2");
            imageSrc.ObjectInstance = imageCollection2;
        }

        private void check()
        {
           if (imageCollection2.Count == 0)
           {
               no_img_db.Visibility = Visibility.Visible;
               no_img_db.FontSize = 30;
               no_img_db.Foreground = Brushes.BlueViolet;
               no_img_db.FontFamily = new FontFamily("Aharoni");
               no_img_db.Content = "NO PICTURES AVAILABLE IN THE DATABASE FOR THIS ALBUM";
           }
           else if (no_img_db.Visibility == Visibility.Visible)
               no_img_db.Visibility = Visibility.Collapsed;
        }

        // On initie le Drag and Drop
        private void ImageDragEvent(object sender, MouseButtonEventArgs e)
        {
            ListBox parent = (ListBox)sender;
            dragSource = parent;
            object data = GetDataFromListBox(dragSource, e.GetPosition(parent));
            if (data != null)
            {
                DragDrop.DoDragDrop(parent, data, DragDropEffects.Move);
            }
        }

        // On ajoute l'objet dans la nouvelle ListBox et on le supprime de l'ancienne
        private void ImageDropEvent(object sender, DragEventArgs e)
        {
            ListBox parent = (ListBox)sender;
            if (dragSource != parent)
            {
                ImageObject data = (ImageObject)e.Data.GetData(typeof(ImageObject));
                ((IList)parent.ItemsSource).Add(data);
                // -1 pour pas le comparer avec lui même car il à été ajouté automatiquement dans imageCollection avec le drag&drop
                for (int i = 0; i < imageCollection2.Count-1; i++)
                 {
                     if (imageCollection2.ElementAt(i).Name.TrimEnd().Equals(data.Name.TrimEnd()))
                     {
                          MessageBoxResult result = MessageBox.Show("File of name '" + data.Name.TrimEnd() + "' already exists. Do you really want to replace it from DB ?", "Caption", MessageBoxButton.YesNo);
                          if (result == MessageBoxResult.Yes)
                          {
                              MainWindow.image_client.Delete(MainWindow.image_client.Get_Image_ID(idAlbum, data.Name), idAlbum);
                              addImage(data.Image, data.Name, this.idAlbum);
                              imageCollection2.RemoveAt(i);
                              imageCollection2.Add(data);
                              return;
                          }
                          else
                              imageCollection2.RemoveAt(imageCollection2.Count - 1);
                     }
                 }
                 if (no_img_db.Visibility == Visibility.Visible)
                     no_img_db.Visibility = Visibility.Collapsed;
                 addImage(data.Image, data.Name, this.idAlbum);
            }
        }

        private void ImageDropLocalEvent(object sender, DragEventArgs e)
        {
            ListBox parent = (ListBox)sender;
            if (dragSource != parent)
            {
                Dictionary<string, byte[]> files = new Dictionary<string, byte[]>();
                DirectoryInfo dirInfo = new DirectoryInfo(PATH);
                IEnumerable<FileInfo> filesInfo = dirInfo.EnumerateFiles();
                ImageObject data = (ImageObject)e.Data.GetData(typeof(ImageObject));
                foreach (FileInfo fi in filesInfo.ToList())
                {
                    
                    string tmp = fi.FullName;
                    string[] tabTmp = tmp.Split('\\');
                    if ((tabTmp[tabTmp.Length - 1].ToLower()).Equals(data.Name.TrimEnd() + ".jpg"))
                    {
                        MessageBoxResult result = MessageBox.Show("File of name '" + data.Name.TrimEnd() + "' already exists. Do you really want to remove it from local repository ?", "Caption", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            ((IList)parent.ItemsSource).Add(data);
                            writeInFile(PATH + "\\" + data.Name.TrimEnd() + ".jpg", data.Image);
                            local_maj(); 
                        }
                        return;
                    }
                }
                ((IList)parent.ItemsSource).Add(data);
                writeInFile(PATH + "\\" + data.Name.TrimEnd() + ".jpg", data.Image);
                local_maj();
            }
        }

        private void addImage(byte[] img, string name, int idAlbum)
        {
            Image_Service.ImageInfo info = new Image_Service.ImageInfo();
            info.ID = MainWindow.currentId;
            info.Album = idAlbum;
            info.Name = name.Split('.')[0];
            MemoryStream image = new MemoryStream(img);
            MainWindow.image_client.Add(info, image);
        }

        // On récupére l'objet que que l'on a dropé
        private static object GetDataFromListBox(ListBox source, Point point)
        {
            UIElement element = (UIElement)source.InputHitTest(point);
            if (element != null)
            {
                object data = DependencyProperty.UnsetValue;
                while (data == DependencyProperty.UnsetValue)
                {
                    data =
                    source.ItemContainerGenerator.ItemFromContainer(element);
                    if (data == DependencyProperty.UnsetValue)
                    {
                        element = (UIElement)VisualTreeHelper.GetParent(element);
                    }
                    if (element == source)
                    {
                        return null;
                    }
                }
                if (data != DependencyProperty.UnsetValue)
                {
                    return data;
                }
            }
            return null;
        }

        private void return_click(object sender, RoutedEventArgs e)
        {
            Albums a = new Albums();
            this.Close();
            a.Show();
        }

        private void refresh(object sender, RoutedEventArgs e)
        {
            db_maj();
            local_maj();
        }

        private void deleteImage(object sender, MouseButtonEventArgs e)
        {
            ListBox parent = (ListBox)sender;
            ImageObject data = (ImageObject)GetDataFromListBox(parent, e.GetPosition(parent));

            MessageBoxResult result = MessageBox.Show("Do you really want to remove " + data.Name.TrimEnd() + " ?", "Caption", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                MainWindow.image_client.Delete(MainWindow.image_client.Get_Image_ID(idAlbum, data.Name), idAlbum);
                for (int i = 0; i < imageCollection2.Count; i++)
                {
                    if (imageCollection2[i].Name.Equals(data.Name))
                    {
                        imageCollection2.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private void local_maj()
        {
            Dictionary<string, byte[]> files = Read_Images_From_Local_Folder(PATH);
            imageCollection1 = new ImageCollection();
            for (int i = 0; i < files.Values.Count; i++)
            {
                imageCollection1.Add(new ImageObject(files.Keys.ElementAt(i), files.Values.ElementAt(i)));
            }

            // On lie la collectionau ObjectDataProvider déclaré dans le fichier XAML
            ObjectDataProvider imageSrc1 = (ObjectDataProvider)FindResource("ImageCollection1");
            imageSrc1.ObjectInstance = imageCollection1;
        }



        private void db_maj()
        {
            imageCollection2.Clear();
            no_img_db.Visibility = Visibility.Visible;
            no_img_db.FontSize = 30;
            no_img_db.Foreground = Brushes.Crimson;
            no_img_db.FontFamily = new FontFamily("Aharoni");
            no_img_db.Content = "LOADING...";
            imageCollection2 = new ImageCollection();

            Thread th = new Thread(new ParameterizedThreadStart(Read_Images_From_DB));
            th.Start(idAlbum);
        }

        private void Image_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Image imagePopup = new Image();
            imagePopup.Source = ((Image)sender).Source;
            popupImage.Child = imagePopup;
            popupImage.IsOpen = true;

        }

        private void Popup_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ((Popup)sender).IsOpen = false;
        }

        
    }
}