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
using System.IO;
using NavigationDrawerPopUpMenu2.Classes;
using System.Data.SqlClient;

namespace NavigationDrawerPopUpMenu2
{
    /// <summary>
    /// Interaction logic for EventsPage.xaml
    /// </summary>
    public partial class EventsPage : Page
    {
        private User userCrt;
        public EventsPage(User user, string tagS)
        {       
            InitializeComponent();
            userCrt = user;
            MakePanelsInvisible(tagS);
            UpdateEvents(tagS);
        }

        private void UpdateEvents(string tagS)
        {
            int index = 1;
            List<int> creators = new List<int>();
            SqlCommand getEvents = new SqlCommand();
            getEvents.Connection = Statics.conn;
            getEvents.CommandText = "SELECT * FROM Events WHERE Participant1 <> @id AND Participant2 <> @id AND Participant3 <> @id " +
                                    "AND Participant4 <> @id AND Participant5 <> @id AND Participant6 <> @id AND CreatorId <> @id " +
                                    "AND Tags LIKE '" + tagS + "%'";

            getEvents.Parameters.AddWithValue("@id", userCrt.GetId());

            SqlDataReader reader = getEvents.ExecuteReader();
            while(reader.Read() && index <=8)
            {
                if (index ==1)
                {
                    EventTitle.Text = reader[1].ToString();
                    EventTitle.TextWrapping = TextWrapping.Wrap;
                    Description.Text = reader[2].ToString();
                    txtLocation.Content = reader[3].ToString();
                    txtStartingDate.Content = reader[4].ToString();
                    txtStartingHour.Content = reader[5].ToString();
                    SetCreatorImage(int.Parse(reader[6].ToString()), UserPicture);
                    creators.Add(int.Parse(reader[6].ToString()));
                }
                else if (index==2)
                {
                    EventTitle2.Text = reader[1].ToString();
                    EventTitle2.TextWrapping = TextWrapping.Wrap;
                    Description2.Text = reader[2].ToString();
                    txtLocation2.Content = reader[3].ToString();
                    txtStartingDate2.Content = reader[4].ToString();
                    txtStartingHour2.Content = reader[5].ToString();
                    SetCreatorImage(int.Parse(reader[6].ToString()), UserPicture2);
                    creators.Add(int.Parse(reader[6].ToString()));
                }

                else if (index == 3)
                {
                    EventTitle3.Text = reader[1].ToString();
                    EventTitle3.TextWrapping = TextWrapping.Wrap;
                    Description3.Text = reader[2].ToString();
                    txtLocation3.Content = reader[3].ToString();
                    txtStartingDate3.Content = reader[4].ToString();
                    txtStartingHour3.Content = reader[5].ToString();
                    SetCreatorImage(int.Parse(reader[6].ToString()), UserPicture3);
                    creators.Add(int.Parse(reader[6].ToString()));
                }

                else if (index == 4)
                {
                    EventTitle4.Text = reader[1].ToString();
                    EventTitle4.TextWrapping = TextWrapping.Wrap;
                    Description4.Text = reader[2].ToString();
                    txtLocation4.Content = reader[3].ToString();
                    txtStartingDate4.Content = reader[4].ToString();
                    txtStartingHour4.Content = reader[5].ToString();
                    SetCreatorImage(int.Parse(reader[6].ToString()), UserPicture4);
                    creators.Add(int.Parse(reader[6].ToString()));
                }

                else if (index == 5)
                {
                    EventTitle5.Text = reader[1].ToString();
                    EventTitle5.TextWrapping = TextWrapping.Wrap;
                    Description5.Text = reader[2].ToString();
                    txtLocation5.Content = reader[3].ToString();
                    txtStartingDate5.Content = reader[4].ToString();
                    txtStartingHour5.Content = reader[5].ToString();
                    SetCreatorImage(int.Parse(reader[6].ToString()), UserPicture5);
                    creators.Add(int.Parse(reader[6].ToString()));
                }

                else if (index == 6)
                {
                    EventTitle6.Text = reader[1].ToString();
                    EventTitle6.TextWrapping = TextWrapping.Wrap;
                    Description6.Text = reader[2].ToString();
                    txtLocation6.Content = reader[3].ToString();
                    txtStartingDate6.Content = reader[4].ToString();
                    txtStartingHour6.Content = reader[5].ToString();
                    SetCreatorImage(int.Parse(reader[6].ToString()), UserPicture6);
                    creators.Add(int.Parse(reader[6].ToString()));
                }

                else if (index == 7)
                {
                    EventTitle7.Text = reader[1].ToString();
                    EventTitle7.TextWrapping = TextWrapping.Wrap;
                    Description7.Text = reader[2].ToString();
                    txtLocation7.Content = reader[3].ToString();
                    txtStartingDate7.Content = reader[4].ToString();
                    txtStartingHour7.Content = reader[5].ToString();
                    SetCreatorImage(int.Parse(reader[6].ToString()), UserPicture7);
                    creators.Add(int.Parse(reader[6].ToString()));
                }
                else if (index == 8)
                {
                    EventTitle8.Text = reader[1].ToString();
                    EventTitle8.TextWrapping = TextWrapping.Wrap;
                    Description8.Text = reader[2].ToString();
                    txtLocation8.Content = reader[3].ToString();
                    txtStartingDate8.Content = reader[4].ToString();
                    txtStartingHour8.Content = reader[5].ToString();
                    SetCreatorImage(int.Parse(reader[6].ToString()), UserPicture8);
                    creators.Add(int.Parse(reader[6].ToString()));
                }
                index++;
            }
            reader.Close();
            SetBadges(creators);
            SetUsersToolTips(creators);
            SetBadgesToolTips(creators);
        }

        private void SetBadges(List<int> creators)
        {
            for(int i = 0; i < creators.Count; i++)
            {
                int id = 0;
                SqlCommand getBadgeId = new SqlCommand();
                getBadgeId.Connection = Statics.conn;
                getBadgeId.CommandText = "SELECT Badge FROM Users WHERE Id = @id";
                getBadgeId.Parameters.AddWithValue("@id", creators[i]);

                SqlDataReader reader = getBadgeId.ExecuteReader();
                if(reader.Read())
                {
                    id = int.Parse(reader[0].ToString());
                }
                reader.Close();

                if (i==0)
                {
                    SetBadge(UserBadge, id);
                }
                else if (i == 1)
                {
                    SetBadge(UserBadge2, id);
                }
                else if (i == 2)
                {
                    SetBadge(UserBadge3, id);
                }
                else if (i == 3)
                {
                    SetBadge(UserBadge4, id);
                }
                else if (i == 4)
                {
                    SetBadge(UserBadge5, id);
                }
                else if (i == 5)
                {
                    SetBadge(UserBadge6, id);
                }
                else if (i == 6)
                {
                    SetBadge(UserBadge7, id);
                }
                else if (i == 7)
                {
                    SetBadge(UserBadge8, id);
                }
            }
        }

        private void SetUsersToolTips(List<int> creators)
        {
            for(int i = 0; i<creators.Count; i++)
            {
                SqlCommand getName = new SqlCommand();
                getName.Connection = Statics.conn;
                getName.CommandText = "SELECT Username FROM Users WHERE Id = @id";
                getName.Parameters.AddWithValue("@id", creators[i]);
                string name = string.Empty;
                SqlDataReader reader = getName.ExecuteReader();
                if(reader.Read())
                {
                    name = reader[0].ToString();
                }
                reader.Close();

                if(i==0)
                {
                    SetToolTip(name, UserPicture, SampleEvent);
                }
                else if (i == 1)
                {
                    SetToolTip(name, UserPicture2, SampleEvent2);
                }
                else if (i == 2)
                {
                    SetToolTip(name, UserPicture3, SampleEvent3);
                }
                else if (i == 3)
                {
                    SetToolTip(name, UserPicture4, SampleEvent4);
                }
                else if (i == 4)
                {
                    SetToolTip(name, UserPicture5, SampleEvent5);
                }
                else if (i == 5)
                {
                    SetToolTip(name, UserPicture6, SampleEvent6);
                }
                else if (i == 6)
                {
                    SetToolTip(name, UserPicture7, SampleEvent7);
                }
                else if (i == 7)
                {
                    SetToolTip(name, UserPicture8, SampleEvent8);
                }
            }
        }

        private void SetBadgesToolTips(List<int> creators)
        {
            for (int i = 0; i < creators.Count; i++)
            {
                SqlCommand getDescription = new SqlCommand();
                getDescription.Connection = Statics.conn;
                getDescription.CommandText = "SELECT Description FROM Badges INNER JOIN Users ON Badges.Id = Users.Badge WHERE Users.Id = @id";
                getDescription.Parameters.AddWithValue("@id", creators[i]);
                string name = string.Empty;
                SqlDataReader reader = getDescription.ExecuteReader();
                if (reader.Read())
                {
                    name = reader[0].ToString();
                }
                reader.Close();

                if (i == 0)
                {
                    SetToolTip(name, UserBadge, SampleEvent);
                }
                else if (i == 1)
                {
                    SetToolTip(name, UserBadge2, SampleEvent2);
                }
                else if (i == 2)
                {
                    SetToolTip(name, UserBadge3, SampleEvent3);
                }
                else if (i == 3)
                {
                    SetToolTip(name, UserBadge4, SampleEvent4);
                }
                else if (i == 4)
                {
                    SetToolTip(name, UserBadge5, SampleEvent5);
                }
                else if (i == 5)
                {
                    SetToolTip(name, UserBadge6, SampleEvent6);
                }
                else if (i == 6)
                {
                    SetToolTip(name, UserBadge7, SampleEvent7);
                }
                else if (i == 7)
                {
                    SetToolTip(name, UserBadge8, SampleEvent8);
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

        private void MakePanelsInvisible(string tagS)
        {
            int count = 0;
            SqlCommand getEvents = new SqlCommand();
            getEvents.Connection = Statics.conn;
            getEvents.CommandText = "SELECT COUNT(Id) FROM Events WHERE Participant1 <> @id AND Participant2 <> @id AND Participant3 <> @id " +
                                    "AND Participant4 <> @id AND Participant5 <> @id AND Participant6 <> @id AND CreatorId <> @id " +
                                    "AND Tags LIKE '" + tagS + "%'"; ;
            getEvents.Parameters.AddWithValue("@id", userCrt.GetId());

            SqlDataReader reader = getEvents.ExecuteReader();
            if (reader.Read())
            {
                count = int.Parse(reader[0].ToString());
            }
            reader.Close();

            if (count <= 7)
            {
                //GridEventsInsideBorder8.Visibility = Visibility.Hidden;
                B8.Visibility = Visibility.Hidden;
            }
            if (count <= 6)
            {
                //GridEventsInsideBorder7.Visibility = Visibility.Hidden;
                B7.Visibility = Visibility.Hidden;
            }
            if (count <= 5)
            {
                //GridEventsInsideBorder6.Visibility = Visibility.Hidden;
                B6.Visibility = Visibility.Hidden;
            }
            if (count <= 4)
            {
                //GridEventsInsideBorder5.Visibility = Visibility.Hidden;
                B5.Visibility = Visibility.Hidden;
            }
            if (count <= 3)
            {
                //GridEventsInsideBorder4.Visibility = Visibility.Hidden;
                B4.Visibility = Visibility.Hidden;
            }
            if (count <= 2)
            {
                //GridEventsInsideBorder3.Visibility = Visibility.Hidden;
                B3.Visibility = Visibility.Hidden;
            }
            if (count <= 1)
            {
                //GridEventsInsideBorder2.Visibility = Visibility.Hidden;
                B2.Visibility = Visibility.Hidden;
            }
            if (count <= 0)
            {
                //GridEventsInsideBorder1.Visibility = Visibility.Hidden;
                B1.Visibility = Visibility.Hidden;
            }
        }

        private void SetCreatorImage(int id, Image img)//(int.Parse(reader[6].ToString()), UserPicture)
        {
            if (id==1)
            {
                img.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Img_source\boy-1.png"));
            }
            else if (id==2)
            {
                img.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Img_source\boy.png"));
            }
            else if (id==3)
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

        private void LoadCrtEvents()
        {
            int x = -850, y = -650, crtEventIndex = 0;
            List<EventC> list = EventC.LoadEvents(EventType.Default, userCrt);
            foreach (EventC evnt in list)
            {
                CreateEventDisplay(evnt, x, y);
                if (crtEventIndex != 2)
                {
                    y += 225 + 200;
                }
                else
                {
                    y = -650;
                    x += 1700;
                }
                crtEventIndex++;
            }
        }

        private void CreateEventDisplay(EventC evnt, int x, int y)
        {
            Border brd = new Border();
            brd.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#616161"));
            brd.BorderThickness = new Thickness(2, 2, 2, 2);
            brd.CornerRadius = new CornerRadius(20, 20, 20, 20);
            brd.Height = 205;
            brd.Width = 800;
            brd.Margin = new Thickness(x, y, 0, 0);

            Canvas canv = new Canvas();
            canv.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#616161"));
            canv.VerticalAlignment = VerticalAlignment.Top;
            canv.Margin = new Thickness(10, 10, 10, 0);
            canv.Height = 185;

            Grid grd = new Grid();
            brd.Child = grd;
            grd.Children.Add(canv);

            // BitmapImage bmp = new BitmapImage();
            Image img = new Image();
            img.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Img_source\coffee.png"));
            img.Width = 50;
            img.Height = 50;
            img.Margin = new Thickness(40, 5, 0, 0);

            Image bdg = new Image();
            bdg.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Img_source\coffee.png"));
            bdg.Width = 35;
            bdg.Height = 35;
            bdg.Margin = new Thickness(5, 10, 0, 0);

            TextBlock txt = new TextBlock();
            txt.Text = evnt.GetName();
            txt.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF"));
            txt.FontSize = 30;
            txt.Margin = new Thickness(110, 10, 0, 0);

            StackPanel stk = new StackPanel();
            stk.Height = 30;
            stk.Width = 30;
            stk.Margin = new Thickness(600, 23, 0, 0);

            //iconuri...

            Label lblD = new Label();
            lblD.Content = evnt.GetStartingDate().ToString();
            lblD.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF"));
            lblD.FontSize = 16;
            lblD.Margin = new Thickness(640, 10, 0, 0);

            Label lblH = new Label();
            lblH.Content = evnt.GetStartingHour().ToString();
            lblH.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF"));
            lblH.FontSize = 16;
            lblH.Margin = new Thickness(640, 30, 0, 0);

            //iconuri...
            Label lblLoc = new Label();
            lblLoc.Content = evnt.GetStartingHour().ToString();
            lblLoc.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF"));
            lblLoc.FontSize = 16;
            lblLoc.Margin = new Thickness(640, 65, 0, 0);

            Label lblDesc = new Label();
            lblDesc.Content = evnt.GetDescription();
            lblDesc.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF"));
            lblDesc.FontSize = 16;
            lblDesc.Height = 77;
            lblDesc.Width = 575;

            Label lblPartic = new Label();
            lblPartic.Height = 35;
            lblPartic.Width = 575;
            //add icons partic...

            Button btn = new Button();
            btn.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6100"));
            btn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#616161"));
            btn.BorderBrush = null;
            btn.Height = 50;
            btn.Width = 130;
            btn.Content = "Join";

            //StackPanel stk2 = new StackPanel();
            canv.Children.Add(img);
            canv.Children.Add(bdg);
            canv.Children.Add(txt);
            canv.Children.Add(lblD);
            canv.Children.Add(lblH);
            canv.Children.Add(lblLoc);
            canv.Children.Add(lblDesc);
            canv.Children.Add(lblPartic);
            canv.Children.Add(btn);

            BorderEvents.Children.Add(brd);
        }

        private void SetToolTip(string text, Image pb, Canvas container)
        {
            TextBlock txtNume = new TextBlock();
            Panel.SetZIndex(txtNume, 150);
            txtNume.Text = text;
            txtNume.Visibility = Visibility.Hidden;
            txtNume.IsEnabled = false;

            txtNume.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
            txtNume.FontSize = 20;
            txtNume.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
            txtNume.Margin = new Thickness(55, -10, 0, 0);
            container.Children.Add(txtNume);


            pb.MouseEnter += new MouseEventHandler((sender, e) => displayNameAvatarHover(sender, e, txtNume));
            pb.MouseLeave += new MouseEventHandler((sender, e) => hideNameAvatarHover(sender, e, txtNume));
        }

        private void displayNameAvatarHover(object sender, EventArgs e, TextBlock txt)
        {
            txt.IsEnabled = true;
            txt.Visibility = Visibility.Visible;
        }

        private void hideNameAvatarHover(object sender, EventArgs e, TextBlock txt)
        {
            txt.IsEnabled = false;
            txt.Visibility = Visibility.Hidden;
        }

        private void SwitchButton(TextBlock txt, MaterialDesignThemes.Wpf.PackIcon packIcon, Button btn)
        {
            if (txt.Text == "Join")
            {
                txt.Text = "Leave";
                btn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#F78181"));
                packIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Close;
            }
            else if (txt.Text == "Leave")
            {
                txt.Text = "Join";
                btn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#01DFA5"));
                packIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Check;
            }
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            SwitchButton(txtBtn1, IconLogo, Button1);
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            SwitchButton(txtBtn2, IconLogo2, Button2);
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            SwitchButton(txtBtn3, IconLogo3, Button3);
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            SwitchButton(txtBtn4, IconLogo4, Button4);
        }

        private void Button7_Click(object sender, RoutedEventArgs e)
        {
            SwitchButton(txtBtn7, IconLogo7, Button7);
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            SwitchButton(txtBtn5, IconLogo5, Button5);
        }

        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            SwitchButton(txtBtn6, IconLogo6, Button6);
        }

        private void Button8_Click(object sender, RoutedEventArgs e)
        {
            SwitchButton(txtBtn8, IconLogo8, Button8);
        }
    }
}
