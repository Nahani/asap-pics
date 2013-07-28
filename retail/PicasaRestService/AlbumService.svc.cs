using DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace PicasaRestService
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "AlbumService" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez AlbumService.svc ou AlbumService.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class AlbumService : IAlbumService
    {
        private AccesBD_SQL dataAccess = AccesBD_SQL.Instance;

        public bool Add(String name, String idProp)
        {
            return dataAccess.Add_Album(new Album(name, Convert.ToInt32(idProp)));
        }

        public bool Delete(String name, String idProp)
        {
            return dataAccess.Delete_Album(Convert.ToInt32(idProp), name);
        }

        public int Get_Album_ID(String name, String idProp)
        {
            return dataAccess.Get_Id_Album(name, Convert.ToInt32(idProp));
        }

        public List<int> Get_AlbumsID_From_User(String idProp)
        {            
            return dataAccess.Get_AlbumsID_From_User(Convert.ToInt32(idProp));            
        }

        public List<int> Get_AlbumsID_From_Other_Users(String idProp)
        {
            return dataAccess.Get_AlbumsID_From_Other_Users(Convert.ToInt32(idProp));            
        }

        public String Get_Name_Album(String id)
        {
            return dataAccess.Get_Name_Album(Convert.ToInt32(id));
        }
    }
}
