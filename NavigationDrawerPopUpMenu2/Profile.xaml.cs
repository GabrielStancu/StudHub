using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using NavigationDrawerPopUpMenu2.Classes;
using System.IO;

namespace NavigationDrawerPopUpMenu2
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {
        User user;
        public Profile(User crtUser)
        {
            InitializeComponent();
            user = crtUser;
            LoadControls();
            LoadBadges();
            LoadEvents();
        }

        private void LoadControls()
        {
            UserName.Text = user.GetUsername();
            Email.Text = user.GetPassword(); //sunt inversate...
            Rating.Text = user.GetRating().ToString() + "/10";
        }

        private void LoadBadges()
        {
            List<int> badgesIds = new List<int>();

            if (user.GetId() == 0)
            {
                badgesIds.Add(1);
                badgesIds.Add(3);
                badgesIds.Add(4);
                badgesIds.Add(5);
                badgesIds.Add(6);
            }
            else if(user.GetId() == 1)
            {
                badgesIds.Add(6);
                badgesIds.Add(4);
                badgesIds.Add(2);
                badgesIds.Add(1);
                badgesIds.Add(8);
            }
            else if(user.GetId() == 2)
            {
                badgesIds.Add(3);
                badgesIds.Add(1);
                badgesIds.Add(7);
                badgesIds.Add(6);
                badgesIds.Add(8);
            }
            else if (user.GetId() == 3)
            {
                badgesIds.Add(7);
                badgesIds.Add(1);
                badgesIds.Add(6);
                badgesIds.Add(8);
                badgesIds.Add(2);
            }
            else if (user.GetId() == 4)
            {
                badgesIds.Add(4);
                badgesIds.Add(3);
                badgesIds.Add(5);
                badgesIds.Add(8);
                badgesIds.Add(1);
            }

            for (int i = 0; i<5; i++)
            {
                SqlCommand getBadge = new SqlCommand();
                getBadge.Connection = Statics.conn;
                getBadge.CommandText = "SELECT * FROM Badges WHERE Id = @id";
                getBadge.Parameters.AddWithValue("@id", badgesIds[i]);

                SqlDataReader reader = getBadge.ExecuteReader();
                if(reader.Read())
                {
                    Badge badge = new Badge(int.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), null);

                    if (i == 0)
                    {
                        BadgeName1.Text = badge.GetName();
                        BadgeName.Text = badge.GetName();
                        loadLogo(badge.GetId(), BadgePicture1);
                        loadLogo(badge.GetId(), BadgePicture);
                        atasareAvatar(ProfilePicture, user.GetId());
                    }
                    else if (i == 1)
                    {
                        BadgeName2.Text = badge.GetName();
                        loadLogo(badge.GetId(), BadgePicture2);
                    }
                    else if (i == 2)
                    {
                        BadgeName3.Text = badge.GetName();
                        loadLogo(badge.GetId(), BadgePicture3);
                    }
                    else if (i == 3)
                    {
                        BadgeName4.Text = badge.GetName();
                        loadLogo(badge.GetId(), BadgePicture4);
                    }
                    else if (i == 4)
                    {
                        BadgeName5.Text = badge.GetName();
                        loadLogo(badge.GetId(), BadgePicture5);
                    }
                }
                reader.Close();          
            }
        }

        private void loadLogo(int id, Image img)
        {
            if (id == 1)
            {
                img.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Img_source\codeManiac.png"));
            }
            else if (id == 2)
            {
                img.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Img_source\naturalist.png"));
            }
            else if (id == 3)
            {
                img.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Img_source\gamer.png"));
            }
            else if (id == 4)
            {
                img.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Img_source\pool.png"));
            }
            else if (id == 5)
            {
                img.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Img_source\dice.png"));
            }
            else if (id == 6)
            {
                img.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Img_source\movie.png"));
            }
            else if (id == 7)
            {
                img.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Img_source\gym.png"));
            }
            else if (id == 8)
            {
                img.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Img_source\health.png"));
            }
        }

        private void atasareAvatar(Image pbUser, int idAvatar)
        {
            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            if (idAvatar == 1)
                bi3.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Img_source\boy-1.png");
            else if (idAvatar == 2)
                bi3.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Img_source\boy.png");
            else if (idAvatar == 3)
                bi3.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Img_source\man-1.png");
            else if (idAvatar == 4)
                bi3.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Img_source\man-2.png");
            else if (idAvatar == 5)
                bi3.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Img_source\girl-1.png");
            else if (idAvatar == 6)
                bi3.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Img_source\girl.png");
            else if (idAvatar == 7)
                bi3.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Img_source\man-2.png");
            else if (idAvatar == 8)
                bi3.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Img_source\man-3.png");
            else //if (idUser == 9)
                bi3.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Img_source\man-4.png");
            bi3.EndInit();
            pbUser.Stretch = Stretch.Fill;
            pbUser.Source = bi3;
        }

        private void LoadEvents()
        {
            int index = 0;
            SqlCommand getEvents = new SqlCommand();
            getEvents.Connection = Statics.conn;
            getEvents.CommandText = "SELECT * FROM Events WHERE Participant1 = @id OR Participant2 = @id OR Participant3 = @id " +
                                    "OR Participant4 = @id OR Participant5 = @id OR Participant6 = @id OR CreatorId = @id ";
            getEvents.Parameters.AddWithValue("@id", user.GetId());

            SqlDataReader reader = getEvents.ExecuteReader();
            while(reader.Read() && index <2)
            {
                if (index == 0)
                {
                    EventTitle.Text = reader[1].ToString();
                    EventTitle.TextWrapping = TextWrapping.Wrap;
                    Description.Content = reader[2].ToString();
                    txtLocation.Content = reader[3].ToString();
                    txtStartingDate.Content = reader[4].ToString();
                    txtStartingHour.Content = reader[5].ToString();
                    SetCreatorImage(int.Parse(reader[6].ToString()), UserPicture);
                }
                else if (index == 1)
                {
                    EventTitle2.Text = reader[1].ToString();
                    EventTitle2.TextWrapping = TextWrapping.Wrap;
                    Description2.Content = reader[2].ToString();
                    txtLocation2.Content = reader[3].ToString();
                    txtStartingDate2.Content = reader[4].ToString();
                    txtStartingHour2.Content = reader[5].ToString();
                    SetCreatorImage(int.Parse(reader[6].ToString()), UserPicture2);
                }
                index++;
            }
            reader.Close();
            SetBadges();

        }

        private void SetBadges()
        {
            for (int i = 0; i < 2; i++)
            {
                int id = 0;
                SqlCommand getBadgeId = new SqlCommand();
                getBadgeId.Connection = Statics.conn;
                getBadgeId.CommandText = "SELECT Badge FROM Users WHERE Id = @id";
                getBadgeId.Parameters.AddWithValue("@id", user.GetId());

                SqlDataReader reader = getBadgeId.ExecuteReader();
                if (reader.Read())
                {
                    id = int.Parse(reader[0].ToString());
                }
                reader.Close();

                if (i == 0)
                {
                    SetBadge(UserBadge, id);
                }
                else if (i == 1)
                {
                    SetBadge(UserBadge2, id);
                }
            }
        }

        private void SetBadge(Image img, int id)
        {
            if (id == 1)
            {
                img.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Img_source\codeManiac.png"));
            }
            else if (id == 2)
            {
                img.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Img_source\naturalists.png"));
            }
            else if (id == 3)
            {
                img.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Img_source\gamer.png"));
            }
            else if (id == 4)
            {
                img.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Img_source\pool.png"));
            }
            else if (id == 5)
            {
                img.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Img_source\dice.png"));
            }
            else if (id == 6)
            {
                img.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Img_source\movie.png"));
            }
            else if (id == 7)
            {
                img.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Img_source\gym.png"));
            }
            else if (id == 8)
            {
                img.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Img_source\health.png"));
            }
        }

        private void SetCreatorImage(int id, Image img)//(int.Parse(reader[6].ToString()), UserPicture)
        {
            if (id == 1)
            {
                img.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Img_source\boy-1.png"));
            }
            else if (id == 2)
            {
                img.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Img_source\boy.png"));
            }
            else if (id == 3)
            {
                img.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Img_source\man-1.png"));
            }
            else if (id == 4)
            {
                img.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Img_source\man-2.png"));
            }
            else if (id == 5)
            {
                img.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Img_source\man-3.png"));
            }
            else if (id == 6)
            {
                img.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Img_source\girl.png"));
            }
            else if (id == 7)
            {
                img.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Img_source\girl-1.png"));
            }
        }
    }
}
