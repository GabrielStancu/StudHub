using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.Data.SqlClient;

namespace NavigationDrawerPopUpMenu2.Classes
{
    public static class Statics
    {
        public static SqlConnection conn = new SqlConnection();
        public static string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Directory.GetCurrentDirectory() + @"\dbStudent.mdf;Integrated Security=True";
        public static void SetConnectionString()
        {
            Statics.conn.ConnectionString = Statics.connStr;
        }

        public static object SerializeObject(object o)
        {
            if (!o.GetType().IsSerializable)
            {
                return null;
            }

            using (MemoryStream stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, o);
                return Convert.ToBase64String(stream.ToArray());
            }
        }

        public static object DeserializeObject(string serializedString)
        {
            byte[] bytes = Convert.FromBase64String(serializedString);

            using (MemoryStream stream = new MemoryStream(bytes))
            {
                //if (stream != null)
                //    return new BinaryFormatter().Deserialize(stream);
                return null;
            }
        }

        public static Badge GetBadgeById(List<Badge> list, int id)
        {
            foreach (Badge crtBadge in list)
            {
                if(crtBadge.GetId() == id)
                {
                    return crtBadge;
                }
            }
            return null;
        }

        public static int GetMaxId(TabeleDB table)
        {
            string tableName = table.ToString();
            int maxId = 0;

            SqlCommand getMax = new SqlCommand();
            getMax.Connection = Statics.conn;
            getMax.CommandText = "SELECT MAX(Id) FROM [" + tableName + "]";

            SqlDataReader reader = getMax.ExecuteReader();
            if (reader.Read())
            {
                maxId = int.Parse(reader[0].ToString());
                maxId++;
            }
            reader.Close();
            return maxId;
        }
    }
}
