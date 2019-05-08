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
using System.Data.Sql;
using System.Data.SqlClient;

namespace MyLibraryProject
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        string MydbConnect = @"Data Source =.\SQLEXPRESS;Initial Catalog = MyLibrary; Integrated Security = True";

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection MysqlConnect = new SqlConnection(MydbConnect);
            // open connect to DB
            try
            {
                MysqlConnect.Open();
                string MyLoginQuery = "select * from Library_Login where UserName= '" + this.TxtUserName.Text + "' and UserPassWord= '" + this.TxtPassWord.Password +"' ";

                // create the commande for the query with 2 arguments(the query var , sqlconnection var)
                SqlCommand MyCommande = new SqlCommand(MyLoginQuery, MysqlConnect);

                //execute the query
                MyCommande.ExecuteNonQuery();
                SqlDataReader MyReader = MyCommande.ExecuteReader();

                int count = 0;

                while (MyReader.Read())
                {
                    count++;
                }
                // if the the reader of the query which is username and pass word is correct once then:
                if (count==1)
                {
                    //MessageBox.Show("Correct User name & password");
                    MainWindow window = new MainWindow();
                    window.Show();
                    this.Hide();
                }
               /* if (count>1)
                {
                    MessageBox.Show("Duplicate Username and Password, try again");
                }*/

                if (count<1)
                {
                    MessageBox.Show("Incorrect Username and Password!! try again");
                    TxtPassWord.Clear();
                    TxtUserName.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);   
            }
        }

        private void Bt_Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Close();
            this.Close();
        }
    }
}
