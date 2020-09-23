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
    public partial class SignIn : Window
    {
        public SignIn()
        {
            InitializeComponent();
            MouseDown += Window_MouseDown;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Statics.conn.ConnectionString = Statics.connStr;
            Statics.conn.Open();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (txtLogUserName.Text == string.Empty && txtLogPassword.Password == string.Empty)
            {
                MessageBox.Show("No username and no password provided...");
                return;
            }
            else if (txtLogUserName.Text == string.Empty)
            {
                MessageBox.Show("No username provided...");
                return;
            }
            else if (txtLogPassword.Password == string.Empty)
            {
                MessageBox.Show("No password provided...");
                return;
            }

            User user = new User(string.Empty, txtLogUserName.Text, txtLogPassword.Password);
            SqlDataReader reader = user.LoadUserInfo();
            if(user.LogIn(reader[2].ToString(), reader[3].ToString()))
            {
                User userTemp = new User(reader);
                user = userTemp;
                reader.Close();

                MainWindow window = new MainWindow(user);
                //window.MainDisplay.Content = new News();
                this.Hide();
                window.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Login failed...");
                reader.Close();
                txtLogPassword.Password = string.Empty;
                txtLogUserName.Text = string.Empty;
            }

        }

        private void LblSignUp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SignUp window = new SignUp();
            this.Hide();
            window.Show();
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Statics.conn.Close();
        }
    }
}
