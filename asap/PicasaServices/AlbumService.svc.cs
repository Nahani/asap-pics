using System;
using System.Collections.Generic;
using DB;

namespace PicasaServices
{
    public class AlbumService : IAlbumService
    {
        private AccesBD_SQL dataAccess = AccesBD_SQL.Instance;

        public bool Add(String name, int idProp)
        {
            return dataAccess.Add_Album(new Album(name, idProp));
        }

        public bool Delete(String name, int idProp)
        {
            return dataAccess.Delete_Album(idProp, name);
        }

        public int Get_Album_ID(String name, int idProp)
        {
            return dataAccess.Get_Id_Album(name, idProp);
        }
        
        public AlbumsResponse Get_Albums_From_User(int idProp)
        {
            AlbumsResponse resp = new AlbumsResponse();
            resp.Albums = dataAccess.Get_Albums_From_User(idProp);
            return resp;
        }

        public AlbumsResponse Get_Albums_From_Other_Users(int idProp)
        {
            AlbumsResponse resp = new AlbumsResponse();
            resp.Albums = dataAccess.Get_Albums_From_Other_Users(idProp);
            return resp;
        }

        public String Get_Name_From_Album(int idAlbum)
        {
            return dataAccess.Get_Name_From_Album(idAlbum);
        }


    }
}
