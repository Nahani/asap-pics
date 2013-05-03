/**
 * 	Fichier : CreateAlbum.xaml.cs 
 * 
 * 	Version : 1.0.0 
 * 		- Definition des actions disponible sur la fenêtre de création d'un album
 * 
 * 	Auteurs : Théo BOURDIN, Alexandre BOURSIER & Nolan POTIER
 * 	
 * 	Résumé : Code behind de l'interface de création d'un album
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

namespace PicasaWPF
{
    /// <summary>
    /// Logique d'interaction pour CreateAlbum.xaml
    /// </summary>
    public partial class CreateAlbum : Window
    {
        public CreateAlbum()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        public void addAlbum(object sender, EventArgs e)
        {
            string name = textName.Text;

            if (name == "" || name.Equals(null) || !MainWindow.album_client.Add(name, MainWindow.currentId))
            {
                MessageBoxResult result = MessageBox.Show("ERROR : Adding hasn't turned to succees\nPlease try another name");
            }
            else
            {
                MessageBoxResult result;
                int id = MainWindow.album_client.Get_Album_ID(name, MainWindow.currentId);
                result = MessageBox.Show("Album créé !");
                DragAndDrop drag = new DragAndDrop(id);
                this.Close();
                drag.Show();
            }

        }
        public void return_click(object sender, EventArgs e)
        {

            Albums albums = new Albums();
            this.Close();
            albums.Show();

        }
    }
}
