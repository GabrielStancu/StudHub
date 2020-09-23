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

namespace NavigationDrawerPopUpMenu2
{
    /// <summary>
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class Account : Page
    {
        List<float> student = new List<float>();
        List<float> org = new List<float>();

        public Account()
        {
            InitializeComponent();

            student.Add(0.95f);
            student.Add(0.90f);
            student.Add(0.80f);
            student.Add(0.75f);

            student.Add(0.95f);
            student.Add(2.70f);
            student.Add(4.80f);
            student.Add(8.00f);




            org.Add(4.95f);
            org.Add(4.80f);
            org.Add(4.75f);
            org.Add(4.60f);

            org.Add(4.95f);
            org.Add(9.60f);
            org.Add(28.50f);
            org.Add(55.20f);

            LoadStudentFees();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadStudentFees();
        }

        private void LoadStudentFees()
        {
            T1.Text = student[0].ToString()+"$";
            T3.Text = student[1].ToString() + "$";
            T6.Text = student[2].ToString() + "$";
            T12.Text = student[3].ToString() + "$";

            P1.Text = "$ " + student[4].ToString() + " charged";
            P3.Text = "$ " + student[5].ToString() + " charged";
            P6.Text = "$ " + student[6].ToString() + " charged";
            P12.Text = "$ " + student[7].ToString() + " charged";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LoadOrgFees();
        }

        private void LoadOrgFees()
        {
            T1.Text = org[0].ToString() + "$";
            T3.Text = org[1].ToString() + "$";
            T6.Text = org[2].ToString() + "$";
            T12.Text = org[3].ToString() + "$";

            P1.Text = "$ " + org[4].ToString() + " charged";
            P3.Text = "$ " + org[5].ToString() + " charged";
            P6.Text = "$ " + org[6].ToString() + " charged";
            P12.Text = "$ " + org[7].ToString() + " charged";
        }
    }
}
