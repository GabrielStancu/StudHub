using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace NavigationDrawerPopUpMenu2.Classes
{
    public class Badge
    {
        #region Fields
        private readonly int Id;
        private readonly string Name;
        private readonly string Description;
        private readonly byte[] Picture;
        private bool Unlocked; //din cod...
        #endregion

        #region Constructors
        //default
        public Badge(int id, string name, string description, byte[] picture)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Picture = picture;
        }

        //cand preia din baza de date 
        public Badge(SqlDataReader reader)
        {
            this.Id = int.Parse(reader[0].ToString());
            this.Name = reader[1].ToString();
            this.Description = reader[2].ToString();
            this.Picture = Encoding.ASCII.GetBytes(reader[3].ToString());
        }
        #endregion

        #region Methods
        public static List<Badge> LoadBadges(User user)
        {
            List<Badge> allBadges = new List<Badge>();

            SqlCommand loadBadges = new SqlCommand
            {
                Connection = Statics.conn,
                CommandText = "SELECT * FROM Badges"
            };

            SqlDataReader reader = loadBadges.ExecuteReader();
            while (reader.Read())
            {
                Badge badge = new Badge(reader);
                allBadges.Add(badge);

                Random rand = new Random(); //carpit... :))
                int unlocked = rand.Next() % 4;
                if(unlocked == 0) user.GetBadgeList().Add(badge);
            }
            reader.Close();
            return allBadges;
        }

        public void UnlockBadge(User user)
        {
            user.GetBadgeList().Add(this);
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

        public string GetDescription()
        {
            return this.Description;
        }

        public byte[] GetPicture()
        {
            return this.Picture;
        }
        #endregion

        #region Setters
        public void SetUnlocked(bool unlocked)
        {
            this.Unlocked = unlocked;
        }
        #endregion
    }
}
