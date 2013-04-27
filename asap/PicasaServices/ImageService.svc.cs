using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PicasaServices;
using System.IO;
using DB;
using System.Drawing;

namespace PicasaServices
{

    public class ImageService : IImageService
    {
        private AccesBD_SQL dataAccess = AccesBD_SQL.Instance;

        public void Add(ImageUploadRequest data)
        {
            byte[] imageBytes = null;
            MemoryStream imageStreamEnMemoire = new MemoryStream();
            data.ImageData.CopyTo(imageStreamEnMemoire);
            imageBytes = imageStreamEnMemoire.ToArray();
            bool result = dataAccess.Add_Img(new Img(data.ImageInfo.Album, data.ImageInfo.Name, imageBytes));
        }

        public bool Delete(int id, int idAlbum)
        {
            return dataAccess.Delete_Img(id, idAlbum);
        }

        public ImageDownloadResponse Get_Image(ImageDownloadRequest data)
        {
            byte[] imageBytes = dataAccess.Get_Image(data.ImageInfo.ID, data.ImageInfo.Album);
            if (imageBytes == null)
            {
                imageBytes = System.Text.Encoding.Default.GetBytes("null");
            }

            ImageDownloadResponse resp = new ImageDownloadResponse();
            resp.ImageData = new MemoryStream(imageBytes);
            return resp;
        }

        public string Get_Image_Name(int id, int idAlbum)
        {
            return dataAccess.Get_Name_Img(id, idAlbum);
        }

        public int Get_Image_ID(int idAlbum, string name)
        {
            return dataAccess.Get_Id_Img(idAlbum, name);
        }

        public List<ImageDownloadResponse> Get_Images_From_Album(int idAlbum)
        {
            List<byte[]> responses = dataAccess.Get_Image_From_Albums(idAlbum);
            Console.WriteLine(responses.Count);
            List<ImageDownloadResponse> result = new List<ImageDownloadResponse>();
            foreach(byte[] blob in responses)
            {
                ImageDownloadResponse resp = new ImageDownloadResponse();
                if (blob == null)
                {
                    resp.ImageData = new MemoryStream(System.Text.Encoding.Default.GetBytes("null"));
                }
                else
                {
                    resp.ImageData = new MemoryStream(blob);
                }
                result.Add(resp);
            }
            return result;
        }

        public List<int> Get_Images_ID_From_Album(int idAlbum)
        {
            return dataAccess.Get_Image_ID_From_Albums(idAlbum);
        }

    }
}