using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PicasaASP.User_Service;
using PicasaASP.Image_Service;
using PicasaASP.Album_Service;
using System.IO;

namespace PicasaASP
{
    public partial class Image : System.Web.UI.Page
    {
        AlbumServiceClient album_client = new Album_Service.AlbumServiceClient();
        ImageServiceClient image_client = new Image_Service.ImageServiceClient();
        UserServiceClient user_client = new User_Service.UserServiceClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            // On récupére la valeur du paramètre ImageID passé dans l’URL
            int id = Int32.Parse(Request.QueryString["id"]);

            ImageInfo iinfo = new ImageInfo();
            iinfo.ID = id;
            iinfo.Album = Int32.Parse(Request.QueryString["idAlbum"]);
            iinfo.Name = image_client.Get_Image_Name(iinfo.ID, id);
            
            // on récupére notre image là où il faut
            Byte[] bytes = GetBytes(image_client.Get_Image(iinfo));
            // et on crée le contenu de notre réponse à la requête HTTP
            // (ici un contenu de type image)
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "image/jpeg";
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }

        protected byte[] GetBytes(Stream img)
        {
            MemoryStream m = new MemoryStream();
            img.CopyTo(m);
            return m.ToArray();
        }
    }
}