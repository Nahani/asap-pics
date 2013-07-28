using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PicasaRestService
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IImageService" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IImageService
    {

        [OperationContract]
        [WebInvoke(Method = "GET",
           ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Wrapped,
           UriTemplate = "delete/{id}/{idAlbum}")]
        bool Delete(string id, string idAlbum);

        [OperationContract]
        [WebInvoke(Method = "GET",
           ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Wrapped,
           UriTemplate = "get_image_name/{id}/{idAlbum}")]
        string Get_Image_Name(string id, string idAlbum);

        [OperationContract]
        [WebInvoke(Method = "GET",
           ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Wrapped,
           UriTemplate = "get_image_id/{idAlbum}/{name}")]
        int Get_Image_ID(string idAlbum, string name);

        [OperationContract]
        [WebInvoke(Method = "GET",
           ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Wrapped,
           UriTemplate = "get_images_id_from_album/{idAlbum}")]
        List<int> Get_Images_ID_From_Album(string idAlbum);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "add/{idAlbum}/{name}",
        ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        bool Add(string idAlbum, string name, Stream image);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "get/{id}/{idAlbum}",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        Stream Get_Image(string id, string idAlbum);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "get_thumb/{id}/{idAlbum}",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        Stream Get_Thumb(string id, string idAlbum);

       

    }
}
