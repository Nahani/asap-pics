using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace TestImage
{
    class Program
    {
        public static void addImage(String imageID, byte[] image)
        {
            string connectionStr = "Server=POTIER\\SQLEXPRESS;Database=TestDB;Integrated Security=true;";
            //string queryStr = "SELECT * from Etudiant";
            // creation des object SqlConnection, SqlCommand et DataReader
            SqlConnection connection = new SqlConnection(connectionStr);
            //SqlCommand oCommand = new SqlCommand(queryStr, connection);

            try
            {
                // connexion au serveur
                connection.Open();
                // construit la requête
                SqlCommand ajoutImage = new SqlCommand(
                "INSERT INTO Image (id, blob, size) " +
                "VALUES(@id, @Blob, @size)", connection);
                ajoutImage.Parameters.Add("@id", SqlDbType.VarChar, imageID.Length).Value
                = imageID;
                ajoutImage.Parameters.Add("@Blob", SqlDbType.Image, image.Length).Value
                = image;
                ajoutImage.Parameters.Add("@size", SqlDbType.Int).Value = image.Length;
                // execution de la requête
                ajoutImage.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur :" + e.Message);
            }
            finally
            {
                // dans tous les cas on ferme la connexion
                connection.Close();
            }
        }

        // récupération d'une image de la base à l'aide d'un DataReader
        public static byte[] getImage(String imageID)
        {
            string connectionStr = "Server=POTIER\\SQLEXPRESS;Database=TestDB;Integrated Security=true;";
            //string queryStr = "SELECT * from Etudiant";
            // creation des object SqlConnection, SqlCommand et DataReader
            SqlConnection connection = new SqlConnection(connectionStr);
            byte[] blob = null;
            try
            {
                // connexion au serveur
                connection.Open();
                // construit la requête 
                SqlCommand getImage = new SqlCommand(
                "SELECT id,size, blob " +
                "FROM Image " +
                "WHERE id = @id", connection);
                getImage.Parameters.Add("@id", SqlDbType.VarChar, imageID.Length).Value =
                imageID;
                // exécution de la requête et création du reader
                SqlDataReader myReader =
                getImage.ExecuteReader(CommandBehavior.SequentialAccess);
                if (myReader.Read())
                {
                    // lit la taille du blob
                    int size = myReader.GetInt32(1);
                    blob = new byte[size];
                    // récupére le blob de la BDD et le copie dans la variable blob
                    myReader.GetBytes(2, 0, blob, 0, size);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur :" + e.Message);
            }
            finally
            {
                // dans tous les cas on ferme la connexion
                connection.Close();
            }
            return blob;
        }

        static void Main(string[] args)
        {
            /*Bitmap bmp1 = new Bitmap("C:/Users/user/Documents/Serveur d'entreprise/C# .Net/asap-pics/TestImage/lol.jpg");
            MemoryStream ms = new MemoryStream();
            bmp1.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] test = ms.ToArray();
            addImage("13",test);*/

            ///---------------------

            byte[] img = getImage("13");
            MemoryStream ms = new MemoryStream(img);
            Image returnImage = Image.FromStream(ms);
            returnImage.Save("lol2.jpg");


            


        }
    }
}
