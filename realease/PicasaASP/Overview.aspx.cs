using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PicasaASP.Album_Service;
using PicasaASP.Image_Service;
using System.Drawing;
using System.IO;
using PicasaASP.User_Service;
using System.Web.UI.HtmlControls;

namespace PicasaASP
{
    public partial class Overview : System.Web.UI.Page
    {

        AlbumServiceClient album_client = new Album_Service.AlbumServiceClient();
        ImageServiceClient image_client = new Image_Service.ImageServiceClient();
        UserServiceClient user_client = new User_Service.UserServiceClient();
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

            Panel p = new Panel();
            p.HorizontalAlign = new HorizontalAlign();
            int n = 0;
            Table table = new Table();
            TableRow rowImage = new TableRow();
            TableRow rowName = new TableRow();
            Dictionary<System.Web.UI.WebControls.Image, Button> albumsDico = new Dictionary<System.Web.UI.WebControls.Image, Button>();

            foreach (Album a in userAlbums.Albums)
            {
                Button b = new Button();
                b.Click += new EventHandler(this.View_Album);
                b.Text = a.name;
                b.CommandName = Convert.ToString(a.idUser);
                int currentAlbumId = album_client.Get_Album_ID(a.name, currentId);
                int[] tmp = image_client.Get_Images_ID_From_Album(currentAlbumId);
                Random r = new Random();
                int idVignette = r.Next(tmp.Length);

                System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                if (tmp.Length > 0)
                {
                    img.ImageUrl = "Image.aspx?id=" + tmp[idVignette] + "&idAlbum=" + currentAlbumId;
                }
                else
                {
                    img.ImageUrl = "Image.aspx?idAlbum=default";
                }

                img.Width = 150;
                img.Height = 150;
                b.CssClass = "art-button";
                albumsDico.Add(img, b);
               
            }

            foreach (System.Web.UI.WebControls.Image img in albumsDico.Keys)
            {
                n++;
                if (n == 1)
                {
                    rowImage = new TableRow();
                    rowName = new TableRow();
                }
                TableCell cellImage = new TableCell();
                TableCell cellName = new TableCell();

                cellImage.Controls.Add(img);
                cellName.Controls.Add(albumsDico[img]);

                rowImage.Cells.Add(cellImage);
                rowName.Cells.Add(cellName);

                if (n == 4)
                {
                    n = 0;
                    table.Rows.Add(rowImage);
                    table.Rows.Add(rowName);
                }
            }

            if (albumsDico.Keys.Count % 4 != 0)
            {
                table.Rows.Add(rowImage);
                table.Rows.Add(rowName);
            }

            table.HorizontalAlign = HorizontalAlign.Center;
            albums.Controls.Add(table);

            if (userAlbums.Albums.Count() == 0)
                reponseAlbums.InnerText = "you don't own any album";


            AlbumsResponse otherAlbums = album_client.Get_Albums_From_Other_Users(currentId);
            albumsDico = new Dictionary<System.Web.UI.WebControls.Image, Button>();
            table = new Table();
            foreach (Album a in otherAlbums.Albums)
            {
                Button b = new Button();
                b.Click += new EventHandler(this.View_Album);
                b.Text = a.name;
                b.CommandName = Convert.ToString(a.idUser);
                int currentAlbumId = album_client.Get_Album_ID(a.name, a.idUser);
                int[] tmp = image_client.Get_Images_ID_From_Album(currentAlbumId);
                Random r = new Random();
                int idVignette = r.Next(tmp.Length);

                System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                if (tmp.Length > 0)
                {
                    img.ImageUrl = "Image.aspx?id=" + tmp[idVignette] + "&idAlbum=" + currentAlbumId;
                }
                else
                {
                    img.ImageUrl = "Image.aspx?idAlbum=default";
                }
                img.Width = 150;
                img.Height = 150;
                b.CssClass = "art-button";

                albumsDico.Add(img, b);
               
            }

            n = 0;
            foreach (System.Web.UI.WebControls.Image img in albumsDico.Keys)
            {
                n++;
                if (n == 1)
                {
                    rowImage = new TableRow();
                    rowName = new TableRow();
                }
                TableCell cellImage = new TableCell();
                TableCell cellName = new TableCell();

                cellImage.Controls.Add(img);
                cellName.Controls.Add(albumsDico[img]);

                rowImage.Cells.Add(cellImage);
                rowName.Cells.Add(cellName);

                if (n == 4)
                {
                    n = 0;
                    table.Rows.Add(rowImage);
                    table.Rows.Add(rowName);
                }
            }

            if (albumsDico.Keys.Count % 4 != 0)
            {
                table.Rows.Add(rowImage);
                table.Rows.Add(rowName);
            }

            table.HorizontalAlign = HorizontalAlign.Center;
            albumsVisu.Controls.Add(table);

            if (otherAlbums.Albums.Count() == 0)
                reponseOtherAlbums.InnerText = "There is not any album from other users";

        }

        protected void View_Album(object sender, EventArgs e)
        {
            String album_name = ((Button)sender).Text;
            int currentAlbumId = album_client.Get_Album_ID(album_name, Convert.ToInt32(((Button)sender).CommandName));
            int[] ids = image_client.Get_Images_ID_From_Album(currentAlbumId);

            Dictionary<String, HtmlAnchor> imagesAlbum = new Dictionary<String, HtmlAnchor>();
            foreach (int id in ids)
            {
                System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                img.ImageUrl = "Image.aspx?id=" + id + "&idAlbum=" + currentAlbumId;
                img.Width = 300;
                img.Height = 200;
                HtmlAnchor href = new HtmlAnchor();
                href.HRef = img.ImageUrl;
                href.Controls.Add(img);
                href.Target = "_blank";

                imagesAlbum.Add(image_client.Get_Image_Name(currentAlbumId, id), href);


            }
            int n = 0;
            Table table = new Table();
            TableRow rowImage = new TableRow();
            TableRow rowName = new TableRow();
            foreach (String imageName in imagesAlbum.Keys)
            {
                n++;
                if (n == 1)
                {
                    rowImage = new TableRow();
                    rowName = new TableRow();
                }
                TableCell cellImage = new TableCell();
                TableCell cellName = new TableCell();

                cellImage.Controls.Add(imagesAlbum[imageName]);
                cellName.Controls.Add(new LiteralControl(imageName));

                rowImage.Cells.Add(cellImage);
                rowName.Cells.Add(cellName);

                if (n == 4)
                {
                    n = 0;
                    table.Rows.Add(rowImage);
                    table.Rows.Add(rowName);
                }
            }

            if (imagesAlbum.Keys.Count % 4 != 0)
            {
                table.Rows.Add(rowImage);
                table.Rows.Add(rowName);
            }

            table.HorizontalAlign = HorizontalAlign.Center;
            images.Controls.Add(table);
            if (ids.Length == 0)
            {
                reponse.InnerText = "No picture available  from the album '" + album_name + "' - User : '" + user_client.Get_User("", Convert.ToInt32(((Button)sender).CommandName)).login + "'";
            }
            else
            {
                reponse.InnerText = "Images from the album '" + album_name + "' - User : '" + user_client.Get_User("", Convert.ToInt32(((Button)sender).CommandName)).login + "'";
            }
        }
    }
}