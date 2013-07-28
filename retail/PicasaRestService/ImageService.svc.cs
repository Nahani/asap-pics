using DB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PicasaRestService
{

    public class ImageService : IImageService
    {
        private AccesBD_SQL dataAccess = AccesBD_SQL.Instance;

        public bool Delete(string id, string idAlbum)
        {
            return dataAccess.Delete_Img(Convert.ToInt32(id), Convert.ToInt32(idAlbum));
        }

        public string Get_Image_Name(string id, string idAlbum)
        {
            return dataAccess.Get_Name_Img(Convert.ToInt32(idAlbum), Convert.ToInt32(id));
        }

        public int Get_Image_ID(string idAlbum, string name)
        {
            return dataAccess.Get_Id_Img(Convert.ToInt32(idAlbum), name);
        }

        public List<int> Get_Images_ID_From_Album(string idAlbum)
        {
            return dataAccess.Get_Image_ID_From_Albums(Convert.ToInt32(idAlbum));
        }

        public bool Add(string idAlbum, string name, Stream image)
        {
            byte[] imageBytes = null;
            MemoryStream imageStreamEnMemoire = new MemoryStream();
            image.CopyTo(imageStreamEnMemoire);
            imageBytes = imageStreamEnMemoire.ToArray();
            Image img = new Bitmap(imageStreamEnMemoire);
            MemoryStream thumbStreamEnMemoire = new MemoryStream();
            Image thumb = img.GetThumbnailImage(200, 200, null, IntPtr.Zero);
            thumb.Save(thumbStreamEnMemoire, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] thumbBytes = thumbStreamEnMemoire.ToArray();
            bool result = dataAccess.Add_Img(new Img(Convert.ToInt32(idAlbum), name, imageBytes, thumbBytes));
            return result;
        }

        public Stream Get_Image(string id, string idAlbum)
        {
            byte[] imageBytes = dataAccess.Get_Image(Convert.ToInt32(id), Convert.ToInt32(idAlbum));
            if (imageBytes == null)
            {
                imageBytes = System.Text.Encoding.Default.GetBytes("null");
            }

            WebOperationContext.Current.OutgoingResponse.ContentType = "image/png";
            Stream resp = new MemoryStream(imageBytes);
            return resp;
        }

        public Stream Get_Thumb(string id, string idAlbum)
        {
            byte[] imageBytes = dataAccess.Get_Thumb(Convert.ToInt32(id), Convert.ToInt32(idAlbum));
            if (imageBytes == null)
            {
                imageBytes = System.Text.Encoding.Default.GetBytes("null");
            }

            WebOperationContext.Current.OutgoingResponse.ContentType = "image/png";
            Stream resp = new MemoryStream(imageBytes);
            return resp;
        }


    }
}
