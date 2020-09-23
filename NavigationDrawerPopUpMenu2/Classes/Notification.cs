using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace NavigationDrawerPopUpMenu2.Classes
{
    public class Notification
    {
        #region Fields
        private readonly int Id;
        private readonly string Title;
        private readonly string Description;
        private readonly DateTime ExpireDate;
        private readonly List<User> UsersToAppear;
        #endregion

        #region Constructors
        public Notification(int id, string title, string description, DateTime expireDate, List<User> usersToAppear)
        {
            this.Id = id;
            this.Title = title;
            this.Description = description;
            this.ExpireDate = expireDate;
            this.UsersToAppear = usersToAppear;
        }

        public Notification(SqlDataReader reader)
        {
            this.Id = int.Parse(reader[0].ToString());
            this.Title = reader[1].ToString();
            this.Description = reader[2].ToString();
            this.ExpireDate = DateTime.Parse(reader[3].ToString());
            this.UsersToAppear = (List<User>)Statics.DeserializeObject(reader[4].ToString());
        }
        #endregion

        #region Methods

        public static List<Notification> LoadNotifications()
        {
            List<Notification> notificationList = new List<Notification>();

            SqlCommand loadNotifications = new SqlCommand();
            loadNotifications.Connection = Statics.conn;
            loadNotifications.CommandText = "SELECT * FROM Notifications";

            SqlDataReader reader = loadNotifications.ExecuteReader();
            while (reader.Read())
            {
                Notification notif = new Notification(reader);
                notificationList.Add(notif);
            }
            reader.Close();
            return notificationList;
        }

        public void LoadUserNotifications(User user)
        {
            if (this.UsersToAppear.Contains(user))
            {
                user.GetNotificationList().Add(this);
            }
        }

        public void DeleteExpiredNotification(User user)
        {
            if (DateTime.Now < this.ExpireDate)
            {
                user.GetNotificationList().Remove(this);

                SqlCommand updateNotifications = new SqlCommand
                {
                    Connection = Statics.conn,
                    CommandText = "UPDATE User SET NotificatonsList = @list WHERE Id = @id"
                };
                updateNotifications.Parameters.AddWithValue("@list", Statics.SerializeObject(user.GetNotificationList()));
                updateNotifications.Parameters.AddWithValue("@id", user.GetId());
                updateNotifications.ExecuteNonQuery();
            }

            SqlCommand deleteNotification = new SqlCommand
            {
                Connection = Statics.conn,
                CommandText = "DELETE FROM Notification WHERE Id = @id"
            };
            deleteNotification.Parameters.AddWithValue("@id", this.Id);
            deleteNotification.ExecuteNonQuery();
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

        public List<User> GetUsersToAppear()
        {
            return this.UsersToAppear;
        }
        #endregion
    }
}
