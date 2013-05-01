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

namespace PicasaWPF
{
    /// <summary>
    /// Logique d'interaction pour DragAndDrop.xaml
    /// </summary>
    public partial class DragAndDrop : Window
    {
        private ImageCollection imageCollection1;
        private ImageCollection imageCollection2;
        private static string PATH = "C:\\Users\\user\\Pictures\\dossier";
        private int idAlbum;
        private ListBox dragSource = null;


        public DragAndDrop(int IdAlbum)
        {
            InitializeComponent();
            this.idAlbum = IdAlbum;
            // On crée notre collection d'image et on y ajoute deux images
            imageCollection1 = new ImageCollection();
            Dictionary<string, byte[]> files = lireImageRepertoireLocal(PATH);
            for (int i = 0; i < files.Values.Count; i++)
            {
                imageCollection1.Add(new ImageObject(files.Keys.ElementAt(i), files.Values.ElementAt(i)));
            }
            // On lie la collection au ObjectDataProvider déclaré dans le fichier XAML
            ObjectDataProvider imageSource =
            (ObjectDataProvider)FindResource("ImageCollection1");
            imageSource.ObjectInstance = imageCollection1;

            imageCollection2 = new ImageCollection();
            files = lireImageRepertoireDB(idAlbum);
            for (int i = 0; i < files.Values.Count; i++)
            {
                imageCollection2.Add(new ImageObject(files.Keys.ElementAt(i), files.Values.ElementAt(i)));
            }

            // On lie la collectionau ObjectDataProvider déclaré dans le fichier XAML
            ObjectDataProvider imageSrc = (ObjectDataProvider)FindResource("ImageCollection2");
            imageSrc.ObjectInstance = imageCollection2;
        }

        private static void ecrireFichier(string chemin, byte[] file)
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

        private static Dictionary<string, byte[]> lireImageRepertoireLocal(string path)
        {
            Dictionary<string, byte[]> files = new Dictionary<string, byte[]>();
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            IEnumerable<FileInfo> filesInfo = dirInfo.EnumerateFiles();
            foreach (FileInfo fi in filesInfo.ToList())
            {
                string tmp = fi.FullName;
                string[] tabTmp = tmp.Split('.');
                if (tabTmp[tabTmp.Length - 1] == "jpg" || tabTmp[tabTmp.Length - 1] == "png")
                {
                    files.Add(fi.Name.Split('.')[0], ImageObject.lireFichier(fi.FullName));
                }
            }
            return files;
        }

        private Dictionary<string, byte[]> lireImageRepertoireDB(int idAlbum)
        {
            Dictionary<string, byte[]> files = new Dictionary<string, byte[]>();
            int[] idImages = MainWindow.image_client.Get_Images_ID_From_Album(idAlbum);
            foreach (int id in idImages)
            {
                Image_Service.ImageInfo img = new Image_Service.ImageInfo();
                img.ID = id;
                img.Album = idAlbum;
                img.Name = MainWindow.image_client.Get_Image_Name(img.Album, id);
                byte[] bytes = ImageObject.GetBytes(MainWindow.image_client.Get_Image(img));
                files.Add(img.Name ,bytes);
            }
            return files;
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
                addImage(data.Image, data.Name, this.idAlbum);
            }
        }

        private void ImageDropLocalEvent(object sender, DragEventArgs e)
        {
            ListBox parent = (ListBox)sender;
            if (dragSource != parent)
            {
                ImageObject data = (ImageObject)e.Data.GetData(typeof(ImageObject));
                ((IList)parent.ItemsSource).Add(data);
                ecrireFichier(PATH + "\\" + data.Name + ".jpg", data.Image);
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

        private void deleteImage(object sender, MouseButtonEventArgs e)
        {
            ListBox parent = (ListBox)sender;
            ImageObject data = (ImageObject)GetDataFromListBox(parent, e.GetPosition(parent));

            MessageBoxResult result = MessageBox.Show("Do you really want to remove " + data.Name + " ?", "Caption", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                MainWindow.image_client.Delete(MainWindow.image_client.Get_Image_ID(idAlbum,data.Name), idAlbum);
                majDB();
            }
        }

        private void majDB()
        {
            Dictionary<string, byte[]> files = lireImageRepertoireLocal(PATH);
            imageCollection2 = new ImageCollection();
            files = lireImageRepertoireDB(idAlbum);
            for (int i = 0; i < files.Values.Count; i++)
            {
                imageCollection2.Add(new ImageObject(files.Keys.ElementAt(i), files.Values.ElementAt(i)));
            }

            // On lie la collectionau ObjectDataProvider déclaré dans le fichier XAML
            ObjectDataProvider imageSrc = (ObjectDataProvider)FindResource("ImageCollection2");
            imageSrc.ObjectInstance = imageCollection2;
        }
    }
}
