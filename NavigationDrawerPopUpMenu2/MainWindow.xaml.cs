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
using System.IO;
using System.Net;
using NavigationDrawerPopUpMenu2.Classes;

namespace NavigationDrawerPopUpMenu2
{
    public partial class MainWindow : Window
    {
        private Image pbUserCrt = new Image();
        private User userCrt;
        Boolean isNotifOpen = false;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtUserName.Text = userCrt.GetUsername();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        public MainWindow(User user)
        {
            InitializeComponent();
            userCrt = user;
            MouseDown += Window_MouseDown;
            atasareAvatar(ProfilePicture, userCrt.GetId());
            MainDisplay.Content = new News();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Minimized;
            }
            else if (this.WindowState == WindowState.Minimized)
            {
                this.WindowState = WindowState.Normal;
            }
        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void BtnOpenNotifications_Click(object sender, RoutedEventArgs e)
        {
            if (isNotifOpen == false)
            {
                BtnCloseNotifications.Visibility = Visibility.Visible;
            }        
        }

        private void BtnCloseNotifications_Click(object sender, RoutedEventArgs e)
        {
            BtnCloseNotifications.Visibility = Visibility.Collapsed;
        }

        private void BtnClickPofile(object sender, RoutedEventArgs e)
        {
            MainDisplay.Content = new Profile(userCrt);        
        }

        private void BtnClickEvent(object sender, RoutedEventArgs e)
        {
            MainDisplay.Content = new Account();
        }

        private void BtnClickEventSample(object sender, RoutedEventArgs e)
        {
            MainDisplay.Content = new EventsPage(userCrt, string.Empty);
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
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

        private void findPerson(string search)
        {
            SqlCommand searchPerson = new SqlCommand();
            searchPerson.Connection = Statics.conn;
            searchPerson.CommandText = "";
        }

        private void BtnClickPofile1(object sender, RoutedEventArgs e)
        {
            MainDisplay.Content = new EventsPage(userCrt, "study");
        }

        private void BtnClickPofile2(object sender, RoutedEventArgs e)
        {
            MainDisplay.Content = new EventsPage(userCrt, "sport");
        }

        private void BtnClickPofile3(object sender, RoutedEventArgs e)
        {
            MainDisplay.Content = new EventsPage(userCrt, "volunteering"); 
        }

        private void BtnClickPofile4(object sender, RoutedEventArgs e)
        {
            //MainDisplay.Content = new EventsPage(userCrt, "recrutari");
        }

        private void BtnClickPofile5(object sender, RoutedEventArgs e)
        {
            MainDisplay.Content = new EventsPage(userCrt, "entertainment");
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            MainDisplay.Content = new EventsPage(userCrt, txtSearch.Text);
        }

        private void BtnClickNews(object sender, RoutedEventArgs e)
        {
            MainDisplay.Content = new News();
        }
    }
}
