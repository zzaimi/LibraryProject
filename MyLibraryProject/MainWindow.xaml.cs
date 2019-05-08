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
using System.Windows.Shapes;

namespace MyLibraryProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Bt_Add_Click(object sender, RoutedEventArgs e)
        {
            InsertLibraryItems InsertWindow = new InsertLibraryItems();
            InsertWindow.Show();
            this.Close();
        }

        private void Bt_View_Click(object sender, RoutedEventArgs e)
        {
            ViewLibraryItems view = new ViewLibraryItems();
            view.Show();
            this.Close();
        }

        private void Bt_Exit_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Hide();
        }
    }
}
