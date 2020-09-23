using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace NavigationDrawerPopUpMenu2.Classes
{
    public class User
    {
        #region Fields
        private readonly int Id;
        private readonly string Username; 
        private readonly string Password;
        private readonly string Mail;
        private float Rating;
        private int RatingCount;
        private readonly List<User> FriendsList;
        private List<Badge> BadgeList;
        private Badge DisplayedBadge;
        private readonly byte[] ProfilePhoto;
        private List<Notification> NotificationsList;
        private List<EventC> ParticipationEventsList;
        private List<EventC> CreatedEventsList;
        #endregion

        #region Constructors
        //constructor pentru cand isi creeaza contul sau se logheaza (username = string.empty)
        public User(string username, string mail, string password)
        {
            this.Id = Statics.GetMaxId(TabeleDB.Users);
            this.Username = username;
            this.Password = password;
            this.Mail = mail;
            this.Rating = 0;
            this.RatingCount = 0;
            this.FriendsList = new List<User>();
            this.BadgeList = new List<Badge>();
            this.DisplayedBadge = null;
            this.ProfilePhoto = new byte[1000000]; //de scos "numarul magic"
            this.NotificationsList = new List<Notification>();
            this.ParticipationEventsList = new List<EventC>();
            this.CreatedEventsList = new List<EventC>();
        }

        //constructor pentru cand e adus din baza de date
        public User(SqlDataReader reader)
        {
            this.Id = int.Parse(reader[0].ToString());
            this.Username = reader[1].ToString();
            this.Password = reader[2].ToString();
            this.Mail = reader[3].ToString();
            this.Rating = float.Parse(reader[4].ToString());
            //this.RatingCount = int.Parse(reader[9].ToString());
            this.FriendsList = null;// (List<User>)Statics.DeserializeObject(reader[0].ToString());
            this.BadgeList = null;// (List<Badge>)Statics.DeserializeObject(reader[0].ToString());
            this.DisplayedBadge = null;//Statics.GetBadgeById(this.BadgeList, this.Id);
            this.ProfilePhoto = null;// Encoding.ASCII.GetBytes(reader[0].ToString());
            this.NotificationsList = null;// (List<Notification>)Statics.DeserializeObject(reader[0].ToString());
            this.ParticipationEventsList = null;// (List<Event>)Statics.DeserializeObject(reader[0].ToString());
            this.CreatedEventsList = null;// (List<Event>)Statics.DeserializeObject(reader[0].ToString());
        }
        #endregion

        #region Methods
        public bool LogIn(string email, string password)
        {
            if (email == this.Mail && password == this.Password)
                return true;
            return false;
        }

        public SqlDataReader LoadUserInfo()
        {
            SqlCommand retrieveInfo = new SqlCommand
            {
                Connection = Statics.conn,
                CommandText = "SELECT * FROM [Users] WHERE Email = @mail"
            };
            retrieveInfo.Parameters.AddWithValue("@mail", this.Mail);

            SqlDataReader reader = retrieveInfo.ExecuteReader();

            if (reader.Read())
            {
                return reader;
            }
            reader.Close();
            return null;
        }

        public void GiveRating(User user, float rating)
        {
            float newRating = (user.GetRating() * user.GetRatingCount() + rating) / (user.GetRatingCount() + 1);
            user.SetNewRating(newRating);
        }

        public void CreateEvent(string name, DateTime startingDate, DateTime endingDate, DateTime startingHour, DateTime endingHour, string description,
                                List<User> participantsList, int minParticipants, int maxParticipants, string locationAddress)
        {
            EventC crtEvent = new EventC(Statics.GetMaxId(TabeleDB.Events), name, startingDate, endingDate, startingHour, endingHour, description,
                                        participantsList, minParticipants, maxParticipants, locationAddress);
            this.CreatedEventsList.Add(crtEvent);

            SqlCommand addEventDb = new SqlCommand
            {
                Connection = Statics.conn,
                CommandText = "INSERT INTO Event VALUES(@id, @name, @stDate, @endDate, @stHour, @endHour, @desc, " +
                                     "@partic, @minP, @maxP, @addr)"
            };
            addEventDb.Parameters.AddWithValue("@id", crtEvent.GetId());
            addEventDb.Parameters.AddWithValue("@name", crtEvent.GetName());
            addEventDb.Parameters.AddWithValue("@stDate", crtEvent.GetStartingDate());
            addEventDb.Parameters.AddWithValue("@endDate", crtEvent.GetEndingDate());
            addEventDb.Parameters.AddWithValue("@stHour", crtEvent.GetStartingHour());
            addEventDb.Parameters.AddWithValue("@endHour", crtEvent.GetEndingDate());
            addEventDb.Parameters.AddWithValue("@desc", crtEvent.GetDescription());
            addEventDb.Parameters.AddWithValue("@partic", crtEvent.GetParticipantsList());
            addEventDb.Parameters.AddWithValue("@minP", crtEvent.GetMinParticipants());
            addEventDb.Parameters.AddWithValue("@maxP", crtEvent.GetMinParticipants());
            addEventDb.Parameters.AddWithValue("@addr", crtEvent.GetLocationAddress());
            addEventDb.ExecuteNonQuery();

            UpdateUserCreatedEvents();
        }

        private void UpdateUserCreatedEvents()
        {
            SqlCommand updateEvents = new SqlCommand
            {
                Connection = Statics.conn,
                CommandText = "UPDATE User SET CreatedEventsList = @list WHERE Id = @id"
            };
            updateEvents.Parameters.AddWithValue("@list", Statics.SerializeObject(this.GetCreatedEvents()));
            updateEvents.Parameters.AddWithValue("@id", this.Id);
            updateEvents.ExecuteNonQuery();
        }

        private void JoinEvent(EventC crtEvent, bool join)
        {
            if (join == true)
                crtEvent.GetParticipantsList().Add(this);
            else
                crtEvent.GetParticipantsList().Remove(this);

            SqlCommand updateEvent = new SqlCommand
            {
                Connection = Statics.conn,
                CommandText = "UPDATE Event SET ParticipantsList = @list WHERE Id = @id"
            };
            updateEvent.Parameters.AddWithValue("@list", Statics.SerializeObject(crtEvent.GetParticipantsList()));
            updateEvent.Parameters.AddWithValue("@id", crtEvent.GetId());
            updateEvent.ExecuteNonQuery();
        }

        private void InviteFriends(List<User> friends, EventC crtEvent)
        {
            string title = this.Username + " invited you at an event!";
            Notification notification = new Notification(Statics.GetMaxId(TabeleDB.Notifications), title, crtEvent.GetDescription(), crtEvent.GetEndingDate(), friends);

            SqlCommand insertNotification = new SqlCommand
            {
                Connection = Statics.conn,
                CommandText = "INSERT INTO Notification VALUES(@id, @title, @description, @endingDate, @friends"
            };
            insertNotification.Parameters.AddWithValue("@id", notification.GetId());
            insertNotification.Parameters.AddWithValue("@title", notification.GetTitle());
            insertNotification.Parameters.AddWithValue("@description", notification.GetDescription());
            insertNotification.Parameters.AddWithValue("@endingDate", notification.GetExpireDate());
            insertNotification.Parameters.AddWithValue("@friends", Statics.SerializeObject(notification.GetUsersToAppear()));
            insertNotification.ExecuteNonQuery();
        }

        public void AddUserToDb()
        {
            //TODO
        }

        #endregion

        #region Getters

        public int GetId()
        {
            return this.Id;
        }

        public string GetUsername()
        {
            return this.Username;
        }

        public string GetPassword()
        {
            return this.Password;
        }

        public string GetMail()
        {
            return this.Mail;
        }

        public float GetRating()
        {
            return this.Rating;
        }

        public int GetRatingCount()
        {
            return this.RatingCount;
        }

        public List<User> GetFriendsList()
        {
            return this.FriendsList;
        }

        public List<EventC> GetParticipationEvents()
        {
            return this.ParticipationEventsList;
        }

        public List<EventC> GetCreatedEvents()
        {
            return this.CreatedEventsList;
        }

        public Badge GetBadge()
        {
            return this.DisplayedBadge;
        }

        public List<Badge> GetBadgeList()
        {
            return this.BadgeList;
        }

        public List<Notification> GetNotificationList()
        {
            return this.NotificationsList;
        }

        public byte[] GetPicture()
        {
            return this.ProfilePhoto;
        }

        #endregion

        #region Setters
        public void SetBadge(Badge badge)
        {
            this.DisplayedBadge = badge;

            SqlCommand updateBadge = new SqlCommand
            {
                Connection = Statics.conn,
                CommandText = "UPDATE User SET DisplayedBadgeId = @bId WHERE Id = @id"
            };
            updateBadge.Parameters.AddWithValue("@bId", badge.GetId());
            updateBadge.Parameters.AddWithValue("@id", this.Id);
            updateBadge.ExecuteNonQuery();
        }

        private void SetNewRating(float newRating)
        {
            this.Rating = newRating;
            this.RatingCount++;

            SqlCommand updateRating = new SqlCommand
            {
                Connection = Statics.conn,
                CommandText = "UPDATE User SET Rating = @rating, RatingsCount = @count WHERE Id = @id"
            };
            updateRating.Parameters.AddWithValue("@rating", this.Rating);
            updateRating.Parameters.AddWithValue("@ratingsCount", this.RatingCount);
            updateRating.ExecuteNonQuery();
        }
        #endregion
    }
}
