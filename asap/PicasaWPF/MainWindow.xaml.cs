﻿using System;
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
using PicasaWPF.User_Service;

namespace PicasaWPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static AlbumServiceClient album_client = new Album_Service.AlbumServiceClient();
        public static ImageServiceClient image_client = new Image_Service.ImageServiceClient();
        public static UserServiceClient user_client = new User_Service.UserServiceClient();
        public static int currentId = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void connection_Click(object sender, RoutedEventArgs e)
        {
            Login conn = new Login();
            this.Close();
            conn.Show();
        }

        private void subscription_Click(object sender, RoutedEventArgs e)
        {
            Subscription subs = new Subscription();
            this.Close();
            subs.Show();
        }
    }
}
