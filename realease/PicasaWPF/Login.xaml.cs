﻿/**
 * 	Fichier : Login.xaml.cs 
 * 
 * 	Version : 1.0.0 
 * 		- Definition des actions disponible sur la fenêtre de connection
 * 		- Vérification de l'utilisateur
 * 
 * 	Auteurs : Théo BOURDIN, Alexandre BOURSIER & Nolan POTIER
 * 	
 * 	Résumé : Code behind de l'interface de connection
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
    /// Logique d'interaction pour Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.ResizeMode = System.Windows.ResizeMode.NoResize;
        }
        
        public void checkUser(object sender, EventArgs e)
        {
            string login = user.Text;
            string pwd = password.Password;
            if (!MainWindow.user_client.Check_password(login, pwd))
            {
                MessageBoxResult result = MessageBox.Show("ERREUR : Invalid Login/Password combination");
            }
            else
            {
                MainWindow.currentId = MainWindow.user_client.Get_User_ID(login);
                MessageBoxResult result = MessageBox.Show("Connection ran into success.");
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
