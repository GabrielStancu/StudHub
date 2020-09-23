using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace NavigationDrawerPopUpMenu2.Classes
{
    public class EventC
    {
        #region Fields
        private readonly int Id;
        private readonly string Name;
        private readonly DateTime StartingDate;
        private readonly DateTime EndingDate;
        private readonly DateTime StartingHour;
        private readonly DateTime EndingHour;
        private readonly string Description;
        private readonly List<User> ParticipantsList;
        private readonly int MinParticipants;
        private readonly int MaxParticipants;
        private readonly string LocationAddress;
        private List<string> Tags; //doar in cod 
        #endregion

        #region Constructors
        //cand creeaza un eveniment nou
        public EventC(int id, string name, DateTime startingDate, DateTime endingDate, DateTime startingHour, DateTime endingHour,
                        string description, List<User> participantsList, int minParticipants, int maxParticipants, string locationAddress)
        {
            this.Id = id;
            this.Name = name;
            this.StartingDate = startingDate;
            this.EndingDate = endingDate;
            this.StartingHour = startingHour;
            this.EndingHour = endingHour;
            this.Description = description;
            this.ParticipantsList = participantsList;
            this.MinParticipants = minParticipants;
            this.MaxParticipants = maxParticipants;
            this.LocationAddress = locationAddress;
        }

        //cand ia un eveniment din baza de date
        public EventC(SqlDataReader reader)
        {
            this.Id = int.Parse(reader[0].ToString());
            this.Name = reader[1].ToString();
            this.StartingDate = DateTime.Parse(reader[2].ToString());
            this.EndingDate = DateTime.Parse(reader[3].ToString());
            this.StartingHour = DateTime.Parse(reader[4].ToString());
            this.EndingHour = DateTime.Parse(reader[5].ToString());
            this.Description = reader[6].ToString();
            this.ParticipantsList = (List<User>)Statics.DeserializeObject(reader[7].ToString());
            this.MinParticipants = int.Parse(reader[8].ToString());
            this.MaxParticipants = int.Parse(reader[9].ToString());
            this.LocationAddress = reader[10].ToString();
        }
        #endregion

        #region Methods

        public static List<EventC> LoadEvents(EventType type, User user)
        {
            List<EventC> list = new List<EventC>();
            SqlCommand getEvents = new SqlCommand
            {
                Connection = Statics.conn,
                CommandText = "SELECT * FROM Event"
            };

            SqlDataReader reader = getEvents.ExecuteReader();
            while (reader.Read())
            {
                EventC crtEvent = new EventC(reader);
                crtEvent.GetEventType(user);
                list.Add(crtEvent);
            }
            reader.Close();
            return list;
        }

        public EventType GetEventType(User user) //asta se apeleaza din UI, in functie de return type o sa fie incarcata in unul din panouri
        {
            //if (user.GetParticipationEvents().Contains(this))
            //{
            //    user.GetParticipationEvents().Add(this);
            //    return EventType.Participated;
            //}
            //else if (user.GetCreatedEvents().Contains(this))
            //{
            //    user.GetCreatedEvents().Add(this);
            //    return EventType.Created;
            //}
            return EventType.Default;
        }

        private void CreateTagsList()
        {
            this.Tags.Add(this.Description);
            this.Tags.Add(this.Name);
            this.Tags.Add(this.LocationAddress);
        }
        #endregion

        #region Getters
        public int GetId()
        {
            return this.Id;
        }

        public string GetName()
        {
            return this.Name;
        }

        public DateTime GetStartingDate()
        {
            return this.StartingDate;
        }

        public DateTime GetEndingDate()
        {
            return this.EndingDate;
        }

        public DateTime GetStartingHour()
        {
            return this.StartingHour;
        }

        public DateTime GetEndingHour()
        {
            return this.EndingHour;
        }

        public string GetDescription()
        {
            return this.Description;
        }

        public List<User> GetParticipantsList()
        {
            return this.ParticipantsList;
        }

        public int GetMinParticipants()
        {
            return this.MinParticipants;
        }

        public int GetMaxParticipants()
        {
            return this.MaxParticipants;
        }

        public string GetLocationAddress()
        {
            return this.LocationAddress;
        }
        #endregion
    }
}
