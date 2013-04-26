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

        /*public List<Album> Get_Albums_From_User(int idProp)
        {
            return dataAccess.Get_Albums_From_User(idProp);
        }*/

        public int Get_Album_ID(String name, int idProp)
        {
            return dataAccess.Get_Id_Album(name, idProp);
        }



    }
}
