using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace NavigationDrawerPopUpMenu2.Classes
{
    public class News
    {
        #region Fields
        private readonly int Id;
        private readonly string Title;
        private readonly string Description;
        private readonly DateTime ExpireDate;
        #endregion

        #region Constructors
        //default
        public News(int id, string title, string description, DateTime expireDate)
        {
            this.Id = id;
            this.Title = title;
            this.Description = description;
            this.ExpireDate = expireDate;
        }

        //cand preia din baza de date 
        public News(SqlDataReader reader)
        {
            this.Id = int.Parse(reader[0].ToString());
            this.Title = reader[1].ToString();
            this.Description = reader[2].ToString();
            this.ExpireDate = DateTime.Parse(reader[3].ToString());
        }

        //cand creeaza o stire noua
        public News(string title, string description, DateTime expireDate)
        {
            this.Id = Statics.GetMaxId(TabeleDB.News);
            this.Title = title;
            this.Description = description;
            this.ExpireDate = expireDate;
        }
        #endregion

        #region Methods
        public void AddNewsToDb()
        {
            SqlCommand insertNews = new SqlCommand();
            insertNews.Connection = Statics.conn;
            insertNews.CommandText = "INSERT INTO News VALUES(@id, @title, @description, @expDate";
            insertNews.Parameters.AddWithValue("@id", this.Id);
            insertNews.Parameters.AddWithValue("@title", this.Title);
            insertNews.Parameters.AddWithValue("@description", this.Description);
            insertNews.Parameters.AddWithValue("@expDate", this.ExpireDate);
            insertNews.ExecuteNonQuery();
        }

        public static List<News> LoadNews()
        {
            List<News> newsList = new List<News>();

            SqlCommand loadNews = new SqlCommand();
            loadNews.Connection = Statics.conn;
            loadNews.CommandText = "SELECT * FROM News";

            SqlDataReader reader = loadNews.ExecuteReader();
            while (reader.Read())
            {
                News news = new News(reader);
                newsList.Add(news);
            }
            reader.Close();
            return newsList;
        }

        public List<News> DeleteNews(List<News> list)
        {
            if (DateTime.Now < this.ExpireDate)
                list.Remove(this);

            SqlCommand deleteNews = new SqlCommand
            {
                Connection = Statics.conn,
                CommandText = "DELETE FROM News WHERE Id = @id"
            };
            deleteNews.Parameters.AddWithValue("@id", this.Id);
            deleteNews.ExecuteNonQuery();

            return list;      
        }
        #endregion

        #region Getters
        public int GetId()
        {
            return this.Id;
        }

        public string GetTitle()
        {
            return this.Title;
        }

        public string GetDescription()
        {
            return this.Description;
        }

        public DateTime GetExpireDate()
        {
            return this.ExpireDate;
        }
        #endregion
    }
}
