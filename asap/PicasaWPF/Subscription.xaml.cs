/**
 * 	Fichier : Subscription.xaml.cs 
 * 
 * 	Version : 1.0.0 
 * 		- Definition des actions disponible sur la fenêtre de visualisation de l'inscription
 * 		
 * 	Auteurs : Théo BOURDIN, Alexandre BOURSIER & Nolan POTIER
 * 	
 * 	Résumé : Code behind pour l'interface d'inscription
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PicasaWPF
{
    /// <summary>
    /// Logique d'interaction pour Subscription.xaml
    /// </summary>
    public partial class Subscription : Window
    {
        public Subscription()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        public void click_subscription(object sender, EventArgs e)
        {
            string last_name = textLN.Text;
            string first_name = textFN.Text;
            string mail = textMail.Text;
            string login = textLogin.Text;
            string pwd = BoxPass.Password;


            if (!MainWindow.user_client.Add(first_name, last_name, login, mail, pwd, false))
            {
                MessageBoxResult result = MessageBox.Show("ERREUR : Subscription hasn't turned to succeed. Please start again.");
            }
            else
            {
                MainWindow.currentId = MainWindow.user_client.Get_User_ID(login);
                MessageBoxResult result = MessageBox.Show("Subscription succeeded.");
                Albums albums = new Albums();
                this.Close();
                albums.Show();
            }
        }
        public void click_return(object sender, EventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Close();
            main.Show();
        }
    }
}
