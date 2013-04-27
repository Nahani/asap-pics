﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PicasaASP.Album_Service;
using PicasaASP.Image_Service;

namespace PicasaASP
{
    public partial class Overview : System.Web.UI.Page
    {

        AlbumServiceClient album_client = new Album_Service.AlbumServiceClient();
        ImageServiceClient image_client = new Image_Service.ImageServiceClient();
        int currentId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                currentId = (int)Session["id"];
            }
            catch (NullReferenceException nre)
            {
                Response.Write("Please connect first :" + nre.Message);
                Response.Redirect("Welcome.aspx");
            }

            AlbumsResponse userAlbums = album_client.Get_Albums_From_User(currentId);

            foreach (Album a in userAlbums.Albums)
            {
                Button b = new Button();
                b.Click += new EventHandler(this.View_Album);
                b.Text = a.name; 
                ImageDownloadResponse[] tmp = image_client.Get_Images_From_Album(album_client.Get_Album_ID(a.name, currentId));
                Random r = new Random();
                int idVignette = r.Next(tmp.Length) ;
                Panel p = new Panel();
                System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                img.ImageUrl = "Image.aspx?id=" + idVignette;
                img.Width = 150;
                p.Controls.Add(img);
                p.Controls.Add(b);
                albums.Controls.Add(p);
            }
        }

        protected void View_Album(object sender, EventArgs e)
        {
            String nomAlbum = ((Button)sender).Text;
            int currentAlbumId = album_client.Get_Album_ID(nomAlbum, currentId);
            int[] ids = image_client.Get_Images_ID_From_Album(currentAlbumId);
            foreach (int id in ids)
            {
                System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                img.ImageUrl = "Image.aspx?id=" + id;
                img.Width = 250;
                images.Controls.Add(img);
            }
        }
    }
}